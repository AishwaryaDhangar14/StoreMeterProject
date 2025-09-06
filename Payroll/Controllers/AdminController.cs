using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Payroll.Models;
using Payroll.Security;

namespace Payroll.Controllers
{
    public class AdminController : Controller
    {
        private readonly Soft_Entity db = new Soft_Entity();

        public ActionResult Dashboard()
        {
            var model = new AdminDashboardViewModel();

            // KPI Boxes
            model.TotalUsers = db.Users.Count();
            model.TotalStores = db.Stores.Count();
            model.TotalRatings = db.Ratings.Count();
            model.ActiveUsers = db.Users.Count(u => DbFunctions.DiffDays(u.LastLoginDate, DateTime.Now) <= 30);

            // Recent Users
            model.RecentUsers = db.Users
                .OrderByDescending(u => u.CreatedAt)
                .Take(5)
                .Select(u => new RecentUserViewModel
                {
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role, // Assume Role is string
                    CreatedAt = u.CreatedAt
                }).ToList();

            // Recent Stores
            model.RecentStores = db.Stores
                .OrderByDescending(s => s.CreatedAt)
                .Take(5)
                .Select(s => new RecentStoreViewModel
                {
                    Name = s.StoreName,
                    Email = s.Email,
                    Address = s.Address,
                    AverageRating = db.Ratings
                        .Where(r => r.StoreId == s.StoreId)
                        .Select(r => (double?)r.Score).DefaultIfEmpty(0).Average() ?? 0
                }).ToList();

            // Ratings Trend (last 6 months example)
            var ratingsTrendData = db.Ratings
                .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Take(6)
                .Select(g => new
                {
                    MonthLabel = g.Key.Month + "/" + g.Key.Year,
                    Count = g.Count()
                }).ToList();

            model.RatingsTrendLabels = ratingsTrendData.Select(r => r.MonthLabel).ToList();
            model.RatingsTrendData = ratingsTrendData.Select(r => r.Count).ToList();

            // User Role Distribution
            model.UserRolesLabels = new List<string> { "Admin", "Store Owner", "Normal User" };
            model.UserRolesData = new List<int>
            {
                db.Users.Count(u => u.Role == "Admin"),
                db.Users.Count(u => u.Role == "StoreOwner"),
                db.Users.Count(u => u.Role == "User")
            };

            return View(model);
        }

        public ActionResult Users()
        {
            var model = db.Users
                         // .Where(u => u.Role != "StoreOwner")
                          .Select(u => new UserViewModel
                          {
                              UserId = u.UserId,
                              Name = u.Name,
                              Email = u.Email,
                              Address = u.Address,
                              Role = u.Role
                          }).ToList();
            return View(model);
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null) return View(new Users());
            var user = db.Users.Find(id);
            user.PasswordHash = new EncryptDecrypt().Decrypt(user.PasswordHash);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Users model)
        {
            string retMsg = "";
            if (ModelState.IsValid && ValidatePassword(model, ref retMsg))
            {
                if (db.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(model);
                }
                if (model.UserId == 0)
                {
                    model.PasswordHash = new EncryptDecrypt().Encrypt(model.PasswordHash);
                    model.UserId = db.Users.Count() > 0 ? db.Users.Max(m => m.UserId) + 1 : 1;
                    model.CreatedAt = DateTime.Now;
                    model.UpdatedAt = DateTime.Now;
                    db.Users.Add(model);
                }
                else
                {
                    model.UpdatedAt = DateTime.Now;
                    model.PasswordHash = new EncryptDecrypt().Encrypt(model.PasswordHash);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Users");
            }
            ModelState.AddModelError("", retMsg);
            return View(model);
        }

        private bool ValidatePassword(Users record, ref string retMsg)
        {
            string password = record.PasswordHash;

            if (string.IsNullOrEmpty(password))
            {
                retMsg = "Password cannot be empty.";
                return false;
            }

            if (password.Length < 8 || password.Length > 16)
            {
                retMsg = "Password must be 8-16 characters long.";
                return false;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit) || !password.Any(c => "!@#$%^&*()".Contains(c)))
            {
                retMsg = "Password must contain at least one uppercase letter, one number, and one special character (!@#$%^&*()).";
                return false;
            }

            return true;
        }


        public ActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }

        public ActionResult Stores()
        {
            var model = db.Stores.Select(s => new StoreViewModel
            {
                StoreId = s.StoreId,
                StoreName = s.StoreName,
                Email = s.Email,
                Address = s.Address,
                AverageRating = s.Ratings.Any() ? s.Ratings.Average(r => r.Score) : 0
            }).ToList();
            return View(model);
        }

        public ActionResult EditStore(int? id)
        {
            ViewBag.Owners = db.Users.Where(u => u.Role == "StoreOwner").ToList();
            if (id == null) return View(new Stores());
            var store = db.Stores.Find(id);
            if (store == null) return HttpNotFound();
            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStore(Stores model)
        {
            ViewBag.Owners = db.Users.Where(u => u.Role == "StoreOwner").ToList();
            if (ModelState.IsValid)
            {
                if (model.StoreId == 0)
                {
                    model.CreatedAt = DateTime.Now;
                    model.UpdatedAt = DateTime.Now;
                    model.StoreId = db.Stores.Count() > 0 ? db.Stores.Max(m => m.StoreId) + 1 : 1;
                    db.Stores.Add(model);
                }
                else
                {
                    model.UpdatedAt = DateTime.Now;
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Stores");
            }
            ModelState.AddModelError("", "");
            return View(model);
        }

        public ActionResult DeleteStore(int id)
        {
            var store = db.Stores.Find(id);
            if (store != null)
            {
                db.Stores.Remove(store);
                db.SaveChanges();
            }
            return RedirectToAction("Stores");
        }

        public ActionResult UserDetails(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return HttpNotFound();

            List<StoreRating> storeRatings = new List<StoreRating>();

            if (user.Role == "StoreOwner")
            {
                storeRatings = db.Stores
                    .Where(s => s.OwnerId == id)
                    .Select(s => new StoreRating
                    {
                        StoreName = s.StoreName,
                        AverageRating = s.Ratings.Any() ? s.Ratings.Average(r => r.Score) : 0
                    })
                    .ToList();
            }

            ViewBag.StoreRatings = storeRatings;

            return View(user);
        }

        public ActionResult Ratings()
        {
            var storeRatings = db.Stores
                .Select(s => new StoreRatingSummaryViewModel
                {
                    StoreId = s.StoreId,
                    StoreName = s.StoreName,
                    AverageRating = s.Ratings.Any() ? s.Ratings.Average(r => r.Score) : 0,
                    TotalRatings = s.Ratings.Count()
                })
                .ToList();

            return View(storeRatings);
        }


    }
}

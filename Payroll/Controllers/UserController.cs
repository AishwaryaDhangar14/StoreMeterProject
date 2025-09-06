using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Payroll.Models;
using Payroll.Security;

namespace Payroll.Controllers
{
    public class UserController : Controller
    {
        private Soft_Entity db = new Soft_Entity();

        int UserNo = 0;

        public UserController()
        {
            if (System.Web.HttpContext.Current.Session["UserNo"] != null)
            {
                UserNo = int.Parse((string)System.Web.HttpContext.Current.Session["UserNo"].ToString());
            }
        }

        public ActionResult Login(string ID = null)
        {
            LoginModel argM = new LoginModel();
            return View(argM);
        }

        [HttpPost]
        [HandleError(View = "ErrorLogin")]
        public ActionResult Login(LoginModel argM)
        {
            int mCnt = 0;
            FormsAuthentication.SetAuthCookie(argM.UserName, false);

            EncryptDecrypt edObj = new EncryptDecrypt();

            try
            {
                if (!ModelState.IsValid)
                    return View(argM);

                Users userobj = db.Users.Where(d => d.Email == argM.UserName).FirstOrDefault();
                if (userobj == null)
                {
                    ModelState.AddModelError("", "Invalid User ID or Password!");
                    return View(argM);
                }

                var ed = new EncryptDecrypt();
                string decryptedPassword;
                try
                {
                    decryptedPassword = ed.Decrypt(userobj.PasswordHash);
                }
                catch
                {
                    ModelState.AddModelError("", "Password decryption failed.");
                    return View(argM);
                }

                if (argM.Password != decryptedPassword)
                {
                    ModelState.AddModelError("", "Invalid User ID or Password!");
                    return View(argM);
                }

                if (userobj.PasswordHash != null && userobj.PasswordHash != "")
                {
                    string decryptPwd = "";
                    try
                    {
                        decryptPwd = edObj.Decrypt(userobj.PasswordHash);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(argM);
                    }
                    DateTime ld_adt = DateTime.Now;

                    if (argM.Password == decryptPwd)
                    {
                        userobj.LastLoginDate = DateTime.Now;
                        db.Entry(userobj).State = EntityState.Modified;
                        db.SaveChanges();

                        Session["UserNo"] = userobj.UserId;
                        Session["UserName"] = userobj.Name;
                        Session["ActiveDate"] = ld_adt;
                        Session["ActiveDateString"] = ld_adt.ToString("dd/MM/yyyy");
                        Session["userid"] = userobj.Email;
                        Session["UserInfo"] = "UserID : " + argM.UserName;//+ " - Active Date : " + ((DateTime)ld_adt).ToString("dd/MM/yyyy");
                        Session["UserRole"] = userobj.Role;

                        switch (userobj.Role)
                        {
                            case "Admin": return RedirectToAction("Dashboard", "Admin");
                            case "StoreOwner": return RedirectToAction("Dashboard", "Store");
                            default: return RedirectToAction("Stores", "User");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User ID or Password!");
                        return View(argM);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User ID or Password!");
                    return View(argM);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(argM);
            }
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public ActionResult Register()
        {
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users record)
        {
            try
            {
                string retMsg = "";
                if (ModelState.IsValid && ValidatePassword(record, ref retMsg))
                {
                    if (db.Users.Any(u => u.Email == record.Email))
                    {
                        ModelState.AddModelError("", "Email already exists.");
                        return View(record);
                    }

                    record.UserId = db.Users.Count() > 0 ? db.Users.Max(m => m.UserId) + 1 : 1;

                    record.CreatedAt = DateTime.Now;
                    record.UpdatedAt = DateTime.Now;
                    record.Role = "User";
                    record.PasswordHash = new EncryptDecrypt().Encrypt(record.PasswordHash);

                    db.Users.Add(record);
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "User registered successfully! Please login.";
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("", retMsg);
                    return View(record);
                }
            }
            catch (Exception e)
            {
                string ErrorMessage = "";
                while (e != null)
                {
                    ErrorMessage = e.Message;
                    e = e.InnerException;
                }
                ModelState.AddModelError("", ErrorMessage);
                return View(record);
            }
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

        public ActionResult Profile()
        {
            int userId = (int)Session["UserNo"];
            var user = db.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            int userId = (int)Session["UserNo"];
            using (var db = new Soft_Entity())
            {
                var user = db.Users.Find(userId); 

                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View();
                }

                string upsw = new EncryptDecrypt().Decrypt(user.PasswordHash);

                if (upsw != oldPassword) 
                {
                    ModelState.AddModelError("", "Old password is incorrect.");
                    return View();
                }

                if (newPassword != confirmPassword)
                {
                    ModelState.AddModelError("", "New password and confirm password do not match.");
                    return View();
                }

                if (newPassword.Length < 8)
                {
                    ModelState.AddModelError("", "Password must be at least 8 characters long.");
                    return View();
                }

                user.PasswordHash = new EncryptDecrypt().Encrypt(newPassword);
                db.SaveChanges();

                ViewBag.Message = "Password updated successfully.";
            }

            return View();
        }

        public ActionResult EditProfile()
        {
            int userId = (int)Session["UserNo"];
            var user = db.Users.Find(userId);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Users model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Profile not updated");
                return View(model);
            }

            if (db.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "Email already exists.");
                return View(model);
            }

            int userId = (int)Session["UserNo"];
            var user = db.Users.Find(userId);

            if (user == null)
                return HttpNotFound();

            user.Name = model.Name;
            user.Email = model.Email;
            user.Address = model.Address;
            user.UpdatedAt = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Profile");
        }

        public ActionResult Stores()
        {
            int userId = Convert.ToInt32(Session["UserNo"]);

            var stores = db.Stores.AsQueryable();

            var model = stores
                .Select(s => new StoreRatingViewModel
                {
                    StoreId = s.StoreId,
                    StoreName = s.StoreName,
                    Address = s.Address,
                    OverallRating = s.Ratings.Any() ? s.Ratings.Average(r => r.Score) : 0,
                    UserRating = s.Ratings.Where(r => r.UserId == userId)
                                          .Select(r => r.Score)
                                          .FirstOrDefault()
                })
                .ToList();

            return View(model); // will look for Views/User/Stores.cshtml
        }

        [HttpPost]
        public JsonResult SubmitRating(int storeId, int rating)
        {
            int userId = Convert.ToInt32(Session["UserNo"]);

            var existing = db.Ratings.FirstOrDefault(r => r.StoreId == storeId && r.UserId == userId);

            if (existing != null)
            {
                existing.Score = rating;
                existing.UpdatedAt = DateTime.Now;
            }
            else
            {
                
                var newRating = new Ratings
                {
                    RatingId = db.Ratings.Count() > 0 ? db.Ratings.Max(m => m.RatingId) + 1 : 1,
                    StoreId = storeId,
                    UserId = userId,
                    Score = rating,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                db.Ratings.Add(newRating);
            }

            db.SaveChanges();

            var newAverage = db.Ratings
                               .Where(r => r.StoreId == storeId)
                               .Average(r => r.Score);

            return Json(new { success = true, newAverage });
        }

    }
}
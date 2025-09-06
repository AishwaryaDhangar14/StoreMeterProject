using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Payroll.Models;
using System;

public class StoreController : Controller
{
    private readonly Soft_Entity db = new Soft_Entity();

    public ActionResult Dashboard()
    {
        int ownerId = Convert.ToInt32(Session["UserNo"]);

        var stores = db.Stores
                       .Where(s => s.OwnerId == ownerId)
                       .ToList();

        var storeRatings = stores.Select(store => new StoreRatings
        {
            StoreName = store.StoreName,
            Ratings = db.Ratings
                        .Where(r => r.StoreId == store.StoreId)
                        .Select(r => new StoreRatingDetail
                        {
                            UserName = r.User.Name,
                            Score = r.Score,
                            CreatedAt = r.CreatedAt
                        })
                        .ToList(),
            AverageRating = db.Ratings
                             .Where(r => r.StoreId == store.StoreId)
                             .Any() ? db.Ratings.Where(r => r.StoreId == store.StoreId).Average(r => r.Score) : 0
        }).ToList();

        ViewBag.StoreRatings = storeRatings;

        return View();
    }


}

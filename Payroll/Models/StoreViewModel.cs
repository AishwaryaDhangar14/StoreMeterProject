using System;
using System.Collections.Generic;

namespace Payroll.Models
{
    // For Admin to see store ratings
    public class AdminDashboardViewModel
    {
        // KPI Boxes
        public int TotalUsers { get; set; }
        public int TotalStores { get; set; }
        public int TotalRatings { get; set; }
        public int ActiveUsers { get; set; }

        // Recent Activity
        public List<RecentUserViewModel> RecentUsers { get; set; }
        public List<RecentStoreViewModel> RecentStores { get; set; }

        // Charts
        public List<string> RatingsTrendLabels { get; set; } // e.g., ["Jan", "Feb", "Mar"]
        public List<int> RatingsTrendData { get; set; } // e.g., [5, 10, 8]

        public List<string> UserRolesLabels { get; set; } // e.g., ["Admin", "Store Owner", "Normal User"]
        public List<int> UserRolesData { get; set; } // e.g., [2, 5, 20]
    }

    public class RecentUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecentStoreViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double AverageRating { get; set; }
    }
    public class StoreRatingDetail
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class StoreRatings
    {
        public string StoreName { get; set; }
        public double AverageRating { get; set; }
        public List<StoreRatingDetail> Ratings { get; set; }
    }

    public class OwnerRatingViewModel
    {
        public string StoreName { get; set; }
        public double AverageRating { get; set; }
        public List<UserRatingViewModel> Ratings { get; set; }
    }

    public class StoreRatingSummaryViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }

    public class StoreRating
    {
        public string StoreName { get; set; }
        public double AverageRating { get; set; }
    }

    public class UserRatingViewModel
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public string Email { get; set; }
    }

    // For Admin to list users
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }    // User Name
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }    // Admin, User, StoreOwner
    }

    // Admin dashboard stats
   
    // Stores listing for Admin or Users
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double AverageRating { get; set; }
        public int? UserRating { get; set; } // user's submitted rating
    }

    // Rating record
    public class RatingViewModel
    {
        public int RatingId { get; set; }
        public string StoreName { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
    }

    public class StoreRatingViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double OverallRating { get; set; } // Average rating
        public int UserRating { get; set; }       // Logged-in user's rating
    }
}

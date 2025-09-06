using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class Users
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please, Enter Name")]
        [StringLength(60, MinimumLength = 20, ErrorMessage = "Name must be between 20 and 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, Enter Email Id")]
        [EmailAddress(ErrorMessage = "Please, Enter Valid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, Enter Password")]
        public string PasswordHash { get; set; }

        [StringLength(400)]
        [Required(ErrorMessage = "Please, Enter Address")]
        public string Address { get; set; }

        public string Role { get; set; } // Admin, User, StoreOwner
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Stores> StoresOwned { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; } // Ratings submitted by user

    }
}
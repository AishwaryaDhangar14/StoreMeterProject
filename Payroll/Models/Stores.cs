using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class Stores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Please, Enter Store Name"), StringLength(120)]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Please, Enter Email Id")]
        [EmailAddress(ErrorMessage = "Please, Enter Valid Email Format")]
        public string Email { get; set; }

        [StringLength(400)]
        [Required(ErrorMessage = "Please, Enter Address")]
        public string Address { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; } // StoreOwner UserId
        public Users Owner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Ratings> Ratings { get; set; }


    }
}
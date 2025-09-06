using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class Ratings
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RatingId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Score { get; set; }

        [Required]
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        virtual  public Stores Store { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        virtual public Users User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Payroll.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please, Enter User ID")]
        [EmailAddress(ErrorMessage = "Please, Enter Valid Email Format")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please, Enter Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

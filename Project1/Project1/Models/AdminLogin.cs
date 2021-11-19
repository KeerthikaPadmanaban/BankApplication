using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project1.Models
{
    public partial class AdminLogin
    {
        [Display(Name = "Admin ID")]
        public string AdminId { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string AdminPassword { get; set; }
    }
}

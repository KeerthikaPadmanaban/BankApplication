using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project1.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            AccountDetails = new HashSet<AccountDetail>();
            AccountStatuses = new HashSet<AccountStatus>();
        }
        [Key]
        [Display(Name = "User ID")]
        public string Userid { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
        public virtual ICollection<AccountStatus> AccountStatuses { get; set; }
    }
}
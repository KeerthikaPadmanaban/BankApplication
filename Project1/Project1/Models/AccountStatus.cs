using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.Models
{
    public partial class AccountStatus
    {
        public long AccNumber { get; set; }
        public bool? AccStatus { get; set; }
        public string Userid { get; set; }

        public virtual AccountDetail User { get; set; }
    }
}

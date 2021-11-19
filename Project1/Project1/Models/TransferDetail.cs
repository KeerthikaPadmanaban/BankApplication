using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project1.Models
{
    public partial class TransferDetail
    {
        public string Userid { get; set; }
        public long TransactionNumber { get; set; }

        [Display(Name = "From Account Number")]
        public long FromAccNumber { get; set; }

        [Display(Name = "To Account Number")]
        public long ToAccNumber { get; set; }

        [Display(Name = "Deposit Number")]
        public decimal DepositAmount { get; set; }
        public decimal WithdrawAmount { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Type of Transaction")]
        public string TypeOfTransaction { get; set; }
        public string ModeOfTransaction { get; set; }
        public decimal? Balance { get; set; }
        public decimal? CurrentBalance { get; set; }

        public virtual AccountDetail User { get; set; }
    }
}

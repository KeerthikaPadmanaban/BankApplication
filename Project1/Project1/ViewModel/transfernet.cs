using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModel
{
    public class transfernet
    {
        [Key]
        public string Userid { get; set; }
        public long FromAccNumber { get; set; }

        [Display(Name = "To Account Number")]
        public long ToAccNumber { get; set; }

        [Display(Name = "Amount")]
        public decimal TransferAmount { get; set; }
        public long TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Type of Transaction")]
        
        public string TypeOfTransaction { get; set; }
        public string ModeOfTransaction { get; set; }
        
        public decimal? Balance { get; set; }
        


    }
}

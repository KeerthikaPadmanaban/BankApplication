using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModel
{
    public class Statement
    {
        [Key]
        public string Userid { get; set; }
        public long TransactionNumber { get; set; }
        public long FromAccNumber { get; set; }
        public long ToAccNumber { get; set; }

        public decimal TransferAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TypeOfTransaction { get; set; }
    }
}

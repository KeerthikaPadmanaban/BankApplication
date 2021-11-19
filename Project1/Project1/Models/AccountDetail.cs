using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project1.Models
{
    public partial class AccountDetail
    {
        public AccountDetail()
        {
            AccountStatuses = new HashSet<AccountStatus>();
        }
        [DisplayName("Account Number")]
        public long AccNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [DisplayName("User ID")]
        public string Userid { get; set; }

        [DisplayName("Account Type")]
        public string AccType { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime Dob { get; set; }


        [Display(Name = "Aadhar Number:")]
        [Required(ErrorMessage = "Aadhar Number is required.")]
        [RegularExpression(@"^([0-9]{16})$", ErrorMessage = "Invalid Aadhar Number.")]
        public long AadharNumber { get; set; }

        [Display(Name = "PAN Number:")]
        [Required(ErrorMessage = "PAN Number is required.")]
        [StringLength(10, ErrorMessage = "Invalid PAN")]
        public string PanCardNumber { get; set; }

        [DisplayName("Nominee Name")]
        public string NomineeName { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public string MobileNumber { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+))+@(([0-9a-zA-Z][-\w][0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        
        [Display(Name = "Temporary Address")]
        public string TempAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermtAddress { get; set; }
        public string Occupation { get; set; }

        [Display(Name = "Annual Income")]
        public double AnnualIncome { get; set; }

        [Display(Name = "1st Deposit Amount")]
        public decimal Balance { get; set; }


        public virtual ICollection<AccountStatus> AccountStatuses { get; set; }
        public virtual ICollection<TransferDetail> TransferDetails { get; set; }

    }
}
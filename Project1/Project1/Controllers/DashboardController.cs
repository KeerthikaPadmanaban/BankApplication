using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Project1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly onlinebankingContext db;

        public string uid { get; private set; }

        public DashboardController(onlinebankingContext context)
        {
            db = context;
        }


        public IActionResult Logout()
        {
            //TempData["uid"] = TempData["id"];
          HttpContext.Session.Remove("uid");
            return RedirectToAction("Index", "Home");


        }
        public IActionResult Index()
        {
            //TempData["uid"] = TempData["id"];
            ViewBag.uid = HttpContext.Session.GetString("uid");
            return View();
        }


        [HttpGet]
        public IActionResult AccountDetails(AccountDetail Model)

        {
            //string id = this.HttpContext.Session.GetString("id");
            //HttpContext.Session.SetString("id", Model.Userid);

            // userid= HttpContext.Session.GetString("uid");
            string acc = Model.AccNumber.ToString();

            HttpContext.Session.SetString("AccI",acc);
            ViewBag.uid = HttpContext.Session.GetString("uid");
            

            AccountDetail ad = db.AccountDetails.Find(ViewBag.uid);
            return View(ad);
        }
        public IActionResult afterchangepassword()
        {
            //TempData["uid"] = TempData["id"];

            return View();
        }
        public IActionResult ChangePassword()
        {
            //TempData["uid"] = TempData["id"];

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword cp)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
           // var user = new AccountDetail() { Userid = ViewBag.uid, Password = cp.NewPassword, ConfirmPassword = cp.ConfirmPassword };
            cp.Userid = ViewBag.uid;
            var user = new AccountDetail() { Userid = ViewBag.uid, Password = cp.NewPassword, ConfirmPassword = cp.ConfirmPassword };
            var a = from b in db.AccountDetails
                    where b.Userid == cp.Userid && b.Password == cp.Oldpassword && b.Password!=cp.NewPassword  && cp.NewPassword==cp.ConfirmPassword
                    select b;


            if (a.Any())
            {
                db.AccountDetails.Attach(user);
                db.Entry(user).Property(x => x.Password).IsModified = true;
                db.Entry(user).Property(x => x.ConfirmPassword).IsModified = true;
                db.SaveChanges();
                ViewBag.Message = " Password  Change successful";
                return RedirectToAction("afterchangepassword", "Dashboard");


            }
            else
            {
                ViewBag.Message = "Invalid  Password";

                //db.AccountDetails.Attach(user);
                //db.Entry(user).Property(x => x.Password).IsModified = true;
                //db.Entry(user).Property(x => x.ConfirmPassword).IsModified = true;
                //db.SaveChanges();
                //ViewBag.Message = "Password Changed Successfully !!";
                //return RedirectToAction("Index", "Dashboard");

                return RedirectToAction("ChangePassword", "Dashboard");
            }


        }

        public IActionResult Statement()
        {

            // ViewBag.cid = HttpContext.Session.GetString("AccI", acc);
            ViewBag.uid = HttpContext.Session.GetString("uid");
            string c = ViewBag.uid;

            var q = (from p in db.AccountDetails
                     where p.Userid == c
                     select p.AccNumber).FirstOrDefault();
            var k = (from p in db.AccountDetails
                     where p.Userid == c
                     select p.Balance).FirstOrDefault();
            ViewBag.kk = k;
            // long q = 1234567890123456;
            var TransferList = (from p in db.TransferDetails
                               where p.FromAccNumber == q || p.ToAccNumber == q
                               select new { p.Userid,p.TransactionNumber,p.FromAccNumber,p.ToAccNumber,p.TransferAmount
                               ,p.TransactionDate,p.TypeOfTransaction}).ToList();
            List<Statement> stment = new List<Statement>();
            foreach(var item in TransferList) 
            {
                Statement obj = new Statement();
                obj.Userid = item.Userid;
                obj.TransactionNumber = item.TransactionNumber;
                obj.FromAccNumber = item.FromAccNumber;
                obj.ToAccNumber = item.ToAccNumber;
                obj.TransactionDate = item.TransactionDate;
                obj.TypeOfTransaction = item.TypeOfTransaction;
                obj.TransferAmount = item.TransferAmount;
                stment.Add(obj);

            }
            return View(stment);


        }


        
        //public IActionResult AccountStatement(AccountSummary Model)
        //{
        //    ViewBag.uid = HttpContext.Session.GetString("uid");

        //    var q = from p in db.TransferDetails
        //            where p.ToAccNumber == Model.Accnum || p.FromAccNumber == Model.Accnum
        //            select p.TransactionNumber;

        //    List<TransferDetail> state =db.TransferDetails.ToList(); 
          

        //    if (q.Any())
        //    {
                
        //        ViewBag.Message = "Transaction Statement";

        //        return RedirectToAction("AccViewStt",state);
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Invalid Acc Num";
        //        return View();
        //    }

          
        //}
    }
}

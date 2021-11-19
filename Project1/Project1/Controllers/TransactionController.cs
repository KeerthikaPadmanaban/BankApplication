using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

using Project1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly onlinebankingContext db;


        public TransactionController(onlinebankingContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            string c = ViewBag.uid;

            var g = (from p in db.AccountDetails
                     where p.Userid == c
                     select p.Balance).FirstOrDefault();
            ViewBag.gg = g;
            return View();
        }
        public IActionResult transfer()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            string id = ViewBag.uid;

            var q = (from p in db.AccountDetails
                     where p.Userid == id
                     select p.AccNumber).FirstOrDefault();

            ViewBag.FromAccNumber = q;

            return View();
        }

        [HttpPost]
        public IActionResult transfer(transfernet net)
        {


            TransferDetail trans = new TransferDetail();
            AccountDetail account = new AccountDetail();

            // (long) cid = HttpContext.Session.GetString("AccI");
            ViewBag.uid = HttpContext.Session.GetString("uid");

            
            
            net.Userid = ViewBag.uid;

            var q = (from p in db.AccountDetails
                    where  p.Userid== net.Userid
                    select p.AccNumber).FirstOrDefault();



            net.FromAccNumber = q;
            trans.FromAccNumber = q;
           

            ViewBag.FromAccNumber = net.FromAccNumber;

            //ViewBag.FromAccNumber = AccI;



            //if (q !=)
            //{

            //    trans.FromAccNumber = q;
            //        net.FromAccNumber;

            //}
            //else
            //{
            //    ViewBag.Message = "Invalid  From Account Number";

            //    return View();
            //}



            var a = from b in db.AccountDetails
                    where b.AccNumber == net.ToAccNumber && net.ToAccNumber != net.FromAccNumber
                    select b;

            if (a.Any())
            {
                trans.ToAccNumber = net.ToAccNumber;

            }
            else
            {
                ViewBag.Message = "Invalid  To Account Number";

                return View();
            }



            var c = from b in db.AccountDetails
                    where b.AccNumber == net.FromAccNumber && b.Balance >= net.TransferAmount
                    select b;

            if (c.Any())
            {
                trans.TransferAmount = net.TransferAmount;

            }
            else
            {
                ViewBag.Message = "Balance Insufficient in Account";

                return View();
            }


            trans.TransactionDate = net.TransactionDate;

            trans.TypeOfTransaction = net.TypeOfTransaction;

            trans.ModeOfTransaction = net.ModeOfTransaction;

            var bal1 = (from o in db.AccountDetails
                        where o.AccNumber == net.FromAccNumber
                        select o.Balance).FirstOrDefault();

            bal1 = decimal.Subtract(bal1, net.TransferAmount);


            var bal2 = (from o in db.AccountDetails
                        where o.AccNumber == net.ToAccNumber
                        select o.Balance).FirstOrDefault();

            bal2 = decimal.Add(bal2, net.TransferAmount);

            var toacc_userid = (from o in db.AccountDetails
                        where o.AccNumber == net.ToAccNumber
                        select o.Userid).FirstOrDefault();

            if (c.Any())
            {
                
                var user = new AccountDetail() { Userid = net.Userid, Balance = bal1 };
                db.AccountDetails.Attach(user);
                db.Entry(user).Property(x => x.Balance).IsModified = true;
                db.SaveChanges();

                // db.Users.Attach(user);
                //db.Entry(user).Property(x => x.Password).IsModified = true;
                //db.SaveChanges();


                //account =  db.AccountDetails.Find(net.Userid);
                //account.Balance = bal1;
                //db.Entry(account.Balance).State = EntityState.Modified;
                //db.SaveChanges();

            }
            else
            {
                ViewBag.Message = "Balance not updated";

            }

            if (c.Any())
            {

                var user = new AccountDetail() { Userid = toacc_userid, Balance = bal2 };
                db.AccountDetails.Attach(user);
                db.Entry(user).Property(x => x.Balance).IsModified = true;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "Balance not updated";

            }

            trans.Balance = bal1;
            trans.CurrentBalance = bal1;
            trans.DepositAmount = 0;//since not null ---statically given random value
            trans.WithdrawAmount = 0;//since not null ---statically given random value
            trans.Userid=net.Userid;
            trans.ModeOfTransaction = "online";
            trans.TransactionDate = DateTime.Now;

            db.TransferDetails.Add(trans);
           
            db.SaveChanges();

            return RedirectToAction("Index");






        }

    }
}

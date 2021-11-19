using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    public class Forget : Controller
    {
        private readonly onlinebankingContext db;

        

        public Forget(onlinebankingContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forgetpass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forgetpass(ChangePassword cp)
        {
            HttpContext.Session.SetString("Fid", cp.Userid);

            var user = new AccountDetail() { Userid = cp.Userid, EmailId=cp.Email };
            var a = from b in db.AccountDetails
                    where b.Userid == cp.Userid && b.EmailId == cp.Email 
                    select b;


            if (a.Any())
            {
                //db.AccountDetails.Attach(user);
                //db.Entry(user).Property(x => x.Password).IsModified = true;
                //db.Entry(user).Property(x => x.ConfirmPassword).IsModified = true;
                //db.SaveChanges();
                //ViewBag.Message = " Password Change successful";
                return RedirectToAction("Forgetpassc", "Forget");


            }
            else
            {
                ViewBag.Message = "Invalid UserId or Email";

                

                return View();
            }

        }

        public IActionResult Forgetpassc()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forgetpassc(ChangePassword cp)
        {
            ViewBag.Fid = HttpContext.Session.GetString("Fid");


            var user = new AccountDetail() { Userid = ViewBag.Fid, Password=cp.NewPassword , ConfirmPassword=cp.ConfirmPassword };
            


            if (cp.NewPassword==cp.ConfirmPassword)
            {
                db.AccountDetails.Attach(user);
                db.Entry(user).Property(x => x.Password).IsModified = true;
                db.Entry(user).Property(x => x.ConfirmPassword).IsModified = true;
                db.SaveChanges();
                ViewBag.Message = " Password Change successful";
                return RedirectToAction("Login", "Login");


            }
            else
            {
                ViewBag.Message = "Enter Correct Password";


                return View();
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    public class AdminController : Controller
    {
        private readonly onlinebankingContext db;
        public AdminController(onlinebankingContext context)
        {
            db = context;
        }
        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin(AdminLogin admin)
        {
            if (admin.AdminId == null || admin.AdminPassword == null)
            {

                ViewBag.Message = "Username or Password cannot be empty";
                return View();
            }
            else
            {


                if (admin.AdminId == null)
                {

                    ViewBag.Message = "Invalid User";

                    return View();
                }
                else if (admin.AdminPassword == null)
                {
                    ViewBag.Message = "Invalid Password";

                    return View();

                }
                else
                {
                    var q = from p in db.AdminLogins
                            where p.AdminId == admin.AdminId && p.AdminPassword == admin.AdminPassword
                            select p;
                    if (q.Any())
                    {
                        ViewBag.Message = "Login Successfull";
                        return RedirectToAction("Welcome");


                    }
                    else
                    {
                        ViewBag.Message = "Invalid Password";
                        return View();
                    }


                }
            }
        }
    }
}

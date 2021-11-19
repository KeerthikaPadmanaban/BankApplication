using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    public class AccountDetailsController : Controller
    {
        private readonly onlinebankingContext _context;

        public AccountDetailsController(onlinebankingContext context)
        {
            _context = context;
        }

        // GET: AccountDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountDetails.ToListAsync());
        }

        // GET: AccountDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.AccountDetails
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (accountDetail == null)
            {
                return NotFound();
            }

            return View(accountDetail);
        }

        // GET: AccountDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccNumber,Password,ConfirmPassword,Userid,AccType,FirstName,LastName,Dob,AadharNumber,PanCardNumber,NomineeName,MobileNumber,EmailId,TempAddress,PermtAddress,Occupation,AnnualIncome,Balance,NetBanking")] AccountDetail accountDetail)
        {
            var t = (from p in _context.AccountDetails
                     where p.PanCardNumber == accountDetail.PanCardNumber

                     select p.Userid).FirstOrDefault();
            var q = (from p in _context.AccountDetails
                     where p.EmailId == accountDetail.EmailId
                     select p.Userid).FirstOrDefault();
            var s = (from p in _context.AccountDetails
                     where p.AadharNumber == accountDetail.AadharNumber
                     select p.Userid).FirstOrDefault();
            var r = (from p in _context.AccountDetails
                     where p.Userid == accountDetail.Userid
                     select p.Userid).FirstOrDefault();
            if (r==null && t==null && q==null && s==null)
            {
                try
                {




                    if (ModelState.IsValid)
                    {
                        _context.Add(accountDetail);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Login", "Login");

                    }



                }
                catch
                {
                    if(r!=null)
                    {
                        ViewBag.Message = "UserId already Exists! ";
                        return View();
                    }
                    else if (s != null)
                    {
                        ViewBag.Message = "AadharCard already Exists! ";
                        return View();
                    }
                    else if (q != null)
                    {
                        ViewBag.Message = "EmailID already Exists! ";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "PanCard already Exists! ";
                        return View();
                    }




                }
                finally
                {
                    ViewBag.Message = "Try Again!";
                }

            }





            return View(accountDetail);
        }
    
        // GET: AccountDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.AccountDetails.FindAsync(id);
            if (accountDetail == null)
            {
                return NotFound();
            }
            return View(accountDetail);
        }

        // POST: AccountDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AccNumber,Password,ConfirmPassword,Userid,AccType,FirstName,LastName,Dob,AadharNumber,PanCardNumber,NomineeName,MobileNumber,EmailId,TempAddress,PermtAddress,Occupation,AnnualIncome,Balance,NetBanking")] AccountDetail accountDetail)
        {
            if (id != accountDetail.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountDetailExists(accountDetail.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountDetail);
        }

        // GET: AccountDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.AccountDetails
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (accountDetail == null)
            {
                return NotFound();
            }

            return View(accountDetail);
        }

        // POST: AccountDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var accountDetail = await _context.AccountDetails.FindAsync(id);
            _context.AccountDetails.Remove(accountDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountDetailExists(string id)
        {
            return _context.AccountDetails.Any(e => e.Userid == id);
        }

        //private readonly onlinebankingContext db;
        //public AccountDetailsController(onlinebankingContext context)
        //{
        //    db = context;
        //}

        //public IActionResult Admin()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult Admin(AdminLogin admin)
        //{
        //    if (admin.AdminId == null || admin.AdminPassword == null)
        //    {

        //        ViewBag.Message = "Username or Password cannot be empty";
        //        return View();
        //    }
        //    else
        //    {


        //        if (admin.AdminId == null)
        //        {

        //            ViewBag.Message = "Invalid User";

        //            return View();
        //        }
        //        else if (admin.AdminPassword == null)
        //        {
        //            ViewBag.Message = "Invalid Password";

        //            return View();

        //        }
        //        else
        //        {
        //            var q = from p in db.AdminLogins
        //                    where p.AdminId == admin.AdminId && p.AdminPassword == admin.AdminPassword
        //                    select p;
        //            if (q.Any())
        //            {
        //                ViewBag.Message = "Login Successfull";
        //                return RedirectToAction("Index");


        //            }
        //            else
        //            {
        //                ViewBag.Message = "Invalid Password";
        //                return View();
        //            }


        //        }
        //    }
        //}
    }
}

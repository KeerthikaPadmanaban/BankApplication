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
    public class TransferDetailsController : Controller
    {
        private readonly onlinebankingContext _context;

        public TransferDetailsController(onlinebankingContext context)
        {
            _context = context;
        }

        // GET: TransferDetails
        public async Task<IActionResult> Index()
        {
            var onlinebankingContext = _context.TransferDetails.Include(t => t.User);
            return View(await onlinebankingContext.ToListAsync());
        }

        // GET: TransferDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferDetail = await _context.TransferDetails
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TransactionNumber == id);
            if (transferDetail == null)
            {
                return NotFound();
            }

            return View(transferDetail);
        }

        // GET: TransferDetails/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.AccountDetails, "Userid", "Userid");
            return View();
        }

        // POST: TransferDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,TransactionNumber,FromAccNumber,ToAccNumber,DepositAmount,WithdrawAmount,TransferAmount,TransactionDate,TypeOfTransaction,ModeOfTransaction,Balance,CurrentBalance")] TransferDetail transferDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transferDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.AccountDetails, "Userid", "Userid", transferDetail.Userid);
            return View(transferDetail);
        }

        // GET: TransferDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferDetail = await _context.TransferDetails.FindAsync(id);
            if (transferDetail == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.AccountDetails, "Userid", "Userid", transferDetail.Userid);
            return View(transferDetail);
        }

        // POST: TransferDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Userid,TransactionNumber,FromAccNumber,ToAccNumber,DepositAmount,WithdrawAmount,TransferAmount,TransactionDate,TypeOfTransaction,ModeOfTransaction,Balance,CurrentBalance")] TransferDetail transferDetail)
        {
            if (id != transferDetail.TransactionNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferDetailExists(transferDetail.TransactionNumber))
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
            ViewData["Userid"] = new SelectList(_context.AccountDetails, "Userid", "Userid", transferDetail.Userid);
            return View(transferDetail);
        }

        // GET: TransferDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferDetail = await _context.TransferDetails
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TransactionNumber == id);
            if (transferDetail == null)
            {
                return NotFound();
            }

            return View(transferDetail);
        }

        // POST: TransferDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var transferDetail = await _context.TransferDetails.FindAsync(id);
            _context.TransferDetails.Remove(transferDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferDetailExists(long id)
        {
            return _context.TransferDetails.Any(e => e.TransactionNumber == id);
        }
    }
}

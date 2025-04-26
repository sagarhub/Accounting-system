using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ams.Data;
using Ams.Models;
using Ams.ViewModels;
using System.Transactions;

namespace Ams.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
              return _context.payments != null ? 
                          View(await _context.payments.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.payments'  is null.");
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.payments == null)
            {
                return NotFound();
            }

            var payment = await _context.payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PaymentVm();
            vm.ledgers = await _context.ledgers.ToListAsync();

            return View(vm);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ledgers = await _context.ledgers.ToListAsync();
                return View(vm);
            }
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var payment = new Payment();
                payment.PayableLedger = vm.PayableLedger;
                payment.remarks = vm.remarks;
                payment.amount = vm.amount;
                payment.date = vm.date;
                payment.ledger_id = vm.ledger_id;
                _context.payments.Add(payment);
                await _context.SaveChangesAsync();

                var TxnEntry = new Transactions
                {
                    transaction_date = payment.date,
                    dr_ledger = payment.PayableLedger,
                    cr_ledger = payment.ledger_id,
                    amount = payment.amount,
                    remarks = payment.remarks,
                    type = 5,
                };
                _context.transactions.Add(TxnEntry);
                await _context.SaveChangesAsync();
                tx.Complete();

            }
            return RedirectToAction("PaymentReport", "Reports");
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.payments == null)
            {
                return NotFound();
            }

            var payment = await _context.payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,amount,PayableLedger,ledger_id,remarks,user_id,rec_status")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
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
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.payments == null)
            {
                return NotFound();
            }

            var payment = await _context.payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.payments == null)
            {
                return Problem("Entity set 'AppDbContext.payments'  is null.");
            }
            var payment = await _context.payments.FindAsync(id);
            if (payment != null)
            {
                _context.payments.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("PaymentReport", "Reports");
        }

        private bool PaymentExists(int id)
        {
          return (_context.payments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

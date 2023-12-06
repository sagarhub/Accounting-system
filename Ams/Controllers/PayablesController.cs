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
    public class PayablesController : Controller
    {
        private readonly AppDbContext _context;

        public PayablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Payables
        public async Task<IActionResult> Index()
        {
              return _context.payables != null ? 
                          View(await _context.payables.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.payables'  is null.");
        }

        // GET: Payables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.payables == null)
            {
                return NotFound();
            }

            var payable = await _context.payables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payable == null)
            {
                return NotFound();
            }

            return View(payable);
        }

        // GET: Payables/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PaybleVm();
            vm.ledgers = await _context.ledgers.ToListAsync();

            return View(vm);
        }

        // POST: Payables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaybleVm vm)

        {
            if (!ModelState.IsValid)
            {
                vm.ledgers = await _context.ledgers.ToListAsync();
                return View(vm);
            }
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))

            {
                var payable = new Payable();
                payable.PayableLedger = vm.PayableLedger;
                payable.date = vm.date;
                payable.amount = vm.amount;
                payable.remarks = vm.remarks;
                 

                _context.payables.Add(payable);
                await _context.SaveChangesAsync();
                tx.Complete();
            }
            return RedirectToAction("Index");
        }

        // GET: Payables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.payables == null)
            {
                return NotFound();
            }

            var payable = await _context.payables.FindAsync(id);
            if (payable == null)
            {
                return NotFound();
            }
            return View(payable);
        }

        // POST: Payables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,amount,PayableLedger,remarks,user_id,rec_status")] Payable payable)
        {
            if (id != payable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayableExists(payable.Id))
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
            return View(payable);
        }

        // GET: Payables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.payables == null)
            {
                return NotFound();
            }

            var payable = await _context.payables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payable == null)
            {
                return NotFound();
            }

            return View(payable);
        }

        // POST: Payables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.payables == null)
            {
                return Problem("Entity set 'AppDbContext.payables'  is null.");
            }
            var payable = await _context.payables.FindAsync(id);
            if (payable != null)
            {
                _context.payables.Remove(payable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayableExists(int id)
        {
          return (_context.payables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

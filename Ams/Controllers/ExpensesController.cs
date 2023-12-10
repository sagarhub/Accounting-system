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
    public class ExpensesController : Controller
    {
        private readonly AppDbContext _context;
        private object expenses;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
      

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.expenses == null)
            {
                return NotFound();
            }

            var expenses = await _context.expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // GET: Expenses/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ExpensesVm();
            vm.ledgers = await _context.ledgers.ToListAsync();

            return View(vm);
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpensesVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ledgers = await _context.ledgers.ToListAsync();
                return View(vm);
            }
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var expenses = new Expenses();

                expenses.ledger_id = vm.ledger_id;
                expenses.amount = vm.amount;
                expenses.remarks = vm.remarks;
                 expenses.date = vm.date;
                expenses.ExpensesLedger = vm.ExpensesLedger;


                _context.expenses.Add(expenses);
                await _context.SaveChangesAsync();
                tx.Complete();
            }
            return RedirectToAction("ExpensesReport","Reports");
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.expenses == null)
            {
                return NotFound();
            }

            var expenses = await _context.expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }
            return View(expenses);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,amount,ExpensesLedger,ledger_id,remarks,user_id,rec_status")] Expenses expenses)
        {
            if (id != expenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensesExists(expenses.Id))
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
            return View(expenses);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.expenses == null)
            {
                return NotFound();
            }

            var expenses = await _context.expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.expenses == null)
            {
                return Problem("Entity set 'AppDbContext.expenses'  is null.");
            }
            var expenses = await _context.expenses.FindAsync(id);
            if (expenses != null)
            {
                _context.expenses.Remove(expenses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpensesExists(int id)
        {
          return (_context.expenses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ams.Data;
using Ams.Models;
using Ams.View_Models;
using Ams.ViewModels;
using System.Transactions;
using Ams.Repository.Interfaces;

namespace Ams.Controllers
{
    public class IncomesController : Controller
    {
        private readonly AppDbContext _context;

        public IncomesController(AppDbContext context)
        {
            _context = context;
        }
       

        // GET: Incomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.incomes == null)
            {
                return NotFound();
            }

            var income = await _context.incomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // GET: Incomes/Create
        public async Task<IActionResult> Create()
        {
            var vm = new IncomeVm();
            vm.ledgers = await _context.ledgers.ToListAsync();
            
            return View(vm);
        }

        // POST: Incomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ledgers = await _context.ledgers.ToListAsync();
                return View(vm);
            }
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var income = new Income();

                income.ledger_id = vm.ledger_id;
                income.amount = vm.amount;
                income.remarks = vm.remarks;
                income.date = vm.date;
                income.IncomeLedger = vm.IncomeLedger;


           
                _context.incomes.Add(income);
                await _context.SaveChangesAsync();
                

                var TxnEntry = new Transactions
                {
                    dr_ledger = income.ledger_id,
                    cr_ledger = income.IncomeLedger,
                    transaction_date = income.date,
                    
                    amount = income.amount,
                    remarks = income.remarks,
                    type = 1,
                };
                _context.transactions.Add(TxnEntry);
                await _context.SaveChangesAsync();
                tx.Complete();
            }
            return RedirectToAction("IncomeReport","Reports");
        }
          
        

        // GET: Incomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.incomes == null)
            {
                return NotFound();
            }

            var income = await _context.incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            return View(income);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,amount,ledger_id,remarks,user_id,rec_status")] Income income)
        {
            if (id != income.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(income);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeExists(income.Id))
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
            return View(income);
        }

        // GET: Incomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.incomes == null)
            {
                return NotFound();
            }

            var income = await _context.incomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.incomes == null)
            {
                return Problem("Entity set 'AppDbContext.incomes'  is null.");
            }
            var income = await _context.incomes.FindAsync(id);
            if (income != null)
            {
                _context.incomes.Remove(income);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeExists(int id)
        {
          return (_context.incomes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

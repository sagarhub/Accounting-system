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
using System.Transactions;

namespace Ams.Controllers
{
    public class LedgersController : Controller
    {
        private readonly AppDbContext _context;

        public LedgersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ledgers
        public async Task<IActionResult> Index()
        {
              return _context.ledgers != null ? 
                          View(await _context.ledgers.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.ledgers'  is null.");
        }
        public async Task<IActionResult> Create()
        {
            var vm = new LedgerVm();
            vm.parentGroups = await _context.parentGroups.ToListAsync();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult>Create(LedgerVm vm)
        {

            if (!ModelState.IsValid)
            {
                vm.parentGroups = await _context.parentGroups.ToListAsync();
                return View(vm);
            }
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var ledger = new Ledger();

                ledger.Ledger_name = vm.Ledger_name;
                ledger.code = vm.code;
                ledger.Parent_ledgerId = vm.Parent_ledgerId;

                    
                    _context.ledgers.Add(ledger);
                   await _context.SaveChangesAsync();
                tx.Complete();

                }
            return RedirectToAction("Index");

           
            
        
        }
        // GET: Ledgers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ledgers == null)
            {
                return NotFound();
            }

            var ledger = await _context.ledgers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ledger == null)
            {
                return NotFound();
            }

            return View(ledger);
        }

        // GET: Ledgers/Create
      

        // POST: Ledgers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
   

        // GET: Ledgers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ledgers == null)
            {
                return NotFound();
            }

            var ledger = await _context.ledgers.FindAsync(id);
            if (ledger == null)
            {
                return NotFound();
            }
            return View(ledger);
        }

        // POST: Ledgers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ledger_name,code,Created_date,user_id,Parent_ledgerId")] Ledger ledger)
        {
            if (id != ledger.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ledger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LedgerExists(ledger.Id))
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
            return View(ledger);
        }

        // GET: Ledgers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ledgers == null)
            {
                return NotFound();
            }

            var ledger = await _context.ledgers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ledger == null)
            {
                return NotFound();
            }

            return View(ledger);
        }

        // POST: Ledgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ledgers == null)
            {
                return Problem("Entity set 'AppDbContext.ledgers'  is null.");
            }
            var ledger = await _context.ledgers.FindAsync(id);
            if (ledger != null)
            {
                _context.ledgers.Remove(ledger);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LedgerExists(int id)
        {
          return (_context.ledgers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

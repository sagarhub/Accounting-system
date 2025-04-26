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
    public class ReceiptsController : Controller
    {
        private readonly AppDbContext _context;

        public ReceiptsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
              return _context.receipts != null ? 
                          View(await _context.receipts.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.receipts'  is null.");
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.receipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipts/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ReceiptVm();
            vm.ledgers = await _context.ledgers.ToListAsync();

            return View(vm);
        }
        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ledgers = await _context.ledgers.ToListAsync();
                return View(vm);
            }
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var receipt = new Receipt();
                receipt.date = vm.date;
                receipt.amount=vm.amount;
                receipt.amount = vm.amount;
                receipt.ReceivableLedger = vm.ReceivableLedger;
                receipt.ledger_id = vm.ledger_id;
                receipt.remarks = vm.remarks;
                _context.receipts.Add(receipt);
                await _context.SaveChangesAsync();

                var TxnEntry = new Transactions
                {
                    transaction_date = receipt.date,
                    dr_ledger = receipt.ledger_id,
                    cr_ledger = receipt.ReceivableLedger,
                    amount = receipt.amount,
                    remarks = receipt.remarks,
                    type = 6,
                };
                _context.transactions.Add(TxnEntry);
                await _context.SaveChangesAsync();  
                tx.Complete();

            }
            return RedirectToAction("ReceiptReport", "Reports");
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,amount,ReceivableLedger,ledger_id,remarks,user_id,rec_status")] Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.Id))
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
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.receipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.receipts == null)
            {
                return Problem("Entity set 'AppDbContext.receipts'  is null.");
            }
            var receipt = await _context.receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.receipts.Remove(receipt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("ReceiptReport", "Reports");
        }

        private bool ReceiptExists(int id)
        {
          return (_context.receipts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

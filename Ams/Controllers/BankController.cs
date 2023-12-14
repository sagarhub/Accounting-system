using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ams.Data;
using Ams.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ams.View_Models;
using Ams.ViewModels;
using Ams.Provider.Interfaces;

namespace Ams.Controllers
{
    public class BankController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserProvider _currentUserProvider;
        public BankController(AppDbContext context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }
        
        
        
        // GET: User
        public async Task<IActionResult> Index()
        {
            return _context.banks != null ?
                        View(await _context.banks.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.users'  is null.");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BankVm vm)
        { 
            long? CurrentUserId = _currentUserProvider.GetCurrentUserId();
            {
                if (ModelState.IsValid)
                {
                    

                        var bank = new Bank
                        {


                            Bank_name = vm.Bank_name,
                            Account_no = vm.Account_no,
                            Address = vm.Address,
                            Contact = vm.Contact,
                            Created_date = DateTime.UtcNow,
                            Remarks = vm.Remarks,
                            Balance = vm.Balance,
                            user_id = CurrentUserId,

                        };
                        _context.banks.Add(bank);
                    _context.SaveChanges();

                    var LedgerEntry = new Ledger
                    {
                        Ledger_name = bank.Bank_name,
                        Description = bank.Remarks,
                        rec_status = bank.rec_status,
                        BankId = bank.Id
                    };
                    _context.ledgers.Add(LedgerEntry);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }





                return View(vm);


            }
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.banks == null)
            {
                return NotFound();
            }

            var bank = await _context.banks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }
    }
}

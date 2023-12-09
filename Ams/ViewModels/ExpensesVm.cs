using Ams.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ams.ViewModels
{
    public class ExpensesVm
    {
        public DateTime date { get; set; }
        public int amount { get; set; }
        public int ExpensesLedger { get; set; }
        public int ledger_id { get; set; }
        public string remarks { get; set; }
        public int user_id { get; set; }
        
        public List<Ledger> ledgers;
        public SelectList LegerSelectList()
        {
            var FilterLedger = ledgers.Where(x => x.Parent_ledgerId == 0).ToList();
            return new SelectList(
                FilterLedger,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                ledger_id

                );
        }
        public SelectList ExpensesLedgerSelectList()
        {
            var FilterExpensesLedger = ledgers.Where(i => i.Parent_ledgerId != 0).ToList();
            return new SelectList(
                FilterExpensesLedger,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                ExpensesLedger
                );
        }


    }
}

using Ams.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ams.ViewModels
{
    public class PaybleVm
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public int PayableLedger { get; set; }

        public string remarks { get; set; }
        public int user_id { get; set; }
        public int ledger_id { get; set; }
        

        public List<Ledger> ledgers;
     
        public SelectList PayableLedgerSelectList()
        {
            var FilterPayableLEdger = ledgers.Where(i => i.Parent_ledgerId == 4).ToList();
            return new SelectList(
                FilterPayableLEdger,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                PayableLedger
                );
        }
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
    }
}

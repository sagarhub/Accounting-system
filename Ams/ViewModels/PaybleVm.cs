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
        
        

        public List<Ledger> ledgers;
        public SelectList LegerSelectList()
        {
            return new SelectList(
                ledgers,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                PayableLedger
                );
        }
    }
}

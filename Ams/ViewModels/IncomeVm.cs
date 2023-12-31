﻿using Ams.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ams.ViewModels
{
    public class IncomeVm
    {
        public DateTime date { get; set; }
        public int amount { get; set; }
         public int IncomeLedger { get; set; }
        public int ledger_id { get; set; }
        public string remarks { get; set; }
        public int user_id { get; set; }
       
        public List<Ledger> ledgers;
         public SelectList LegerSelectList()
        {
            var FilterLedger = ledgers.Where(x=>x.Parent_ledgerId==0).ToList();
            return new SelectList(
                FilterLedger,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                ledger_id
                
                );
        }
        public SelectList IncomeLedgerSelectList()
        {
            var FilterIncomeLedger = ledgers.Where(i=>i.Parent_ledgerId==1).ToList();
            return new SelectList(
                FilterIncomeLedger,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                IncomeLedger
                );
        }

    }
}

using Ams.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ams.View_Models
{
    public class LedgerVm
    {
        public string Ledger_name { get; set; }
        public int code { get; set; }      
        public int Parent_ledgerId { get; set; }

        public List<ParentGroup> parentGroups;
        public SelectList ParentGroupList()
        {
            return new SelectList
                (
                parentGroups,
                nameof(ParentGroup.id),
                nameof(ParentGroup.name),
                Parent_ledgerId

                );
        }

    }

    }


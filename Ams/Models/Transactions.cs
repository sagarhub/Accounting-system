using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ams.Models
{
    [Table("transactions")]
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public DateTime transaction_date { get; set; }
        public int dr_ledger { get; set; }
        public int cr_ledger { get; set; }

        public int amount { get; set; }
        

        public string remarks { get; set; }
        public int type { get; set; }
        public string rec_status { get; set; } = "A";

    }
}

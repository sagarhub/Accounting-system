using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ams.Models
{
    [Table("receivables")]
    public class Receivable
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public int ReceivableLedger { get; set; }
        public int ledger_id { get; set; }
        public string remarks { get; set; }
        public int user_id { get; set; }
        public string rec_status { get; set; } = "A";

    }
}

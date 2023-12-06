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
        public int dr { get; set; }
        public int cr { get; set; }

        public int ledger_id { get; set; }
        public string remarks { get; set; }

        public string rec_status { get; set; } = "A";

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ams.Models
{
    [Table("Ledgers")]
    public class Ledger
    {
        [Key]
        public int Id { get; set; }
          
        public string Ledger_name { get; set; }
        
        public int code { get; set; }
     
        public DateTime Created_date { get; set; }

        public int user_id { get; set; }
        public int Parent_ledgerId { get; set; }


        

    }
}

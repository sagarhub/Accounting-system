using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ams.Models
{
    [Table("Bank")]
    public class Bank
    {
        [Key]
        public int Id { get; set; }

        public string Bank_name { get; set; }
        public string Account_no { get; set; }
       
        public string Address { get; set; }
        public string Contact { get; set; }
        public int Balance { get; set; }
        public DateTime Created_date { get; set; }

        public int user_id { get; set; }
        public string Remarks { get; set; }
     




    }
}

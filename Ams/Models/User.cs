using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ams.Models
{
    
        
    [Table("User")]
        public class User
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Last_name { get; set; }
            public int Contact { get; set; }

            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
            public string address { get; set; }

        }
    }




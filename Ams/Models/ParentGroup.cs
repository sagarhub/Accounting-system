using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Ams.Models
{
    [Table("ParentGroups")]
    public class ParentGroup
    {
        [Key]
        
        public int id { get; set; }
        public string name { get; set; }
        public int code { get; set; }


    }
}

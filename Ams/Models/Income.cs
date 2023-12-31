﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ams.Models
{
    [Table("income")]
    public class Income
    {
        [Key]
        public int Id { get; set; }
        public DateTime date{get; set;} = DateTime.Now;
        public int amount { get; set; }
        public int IncomeLedger { get; set; }
        public int ledger_id { get; set; }
        public string remarks { get; set; }
        public int user_id { get;set; }
        public string rec_status { get; set; } = "A";

    }
}

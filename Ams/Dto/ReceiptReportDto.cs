namespace Ams.Dto
{
    public class ReceiptReportDto
    {
            public DateTime date { get; set; }
            public string ReceivableLedger { get; set; }
            public string ledger_name { get; set; }
            public decimal amount { get; set; }
            public string remarks { get; set; }
        public decimal current { get; set; }
        }
    }





namespace Ams.Dto
{
    public class PaymentReportDto
    {
        public DateTime date { get; set; }
        public string  PayableLedger { get; set; }
        public string ledger_name { get; set; }    
        public decimal amount { get; set; } 
        public string remarks { get; set; }
    }
}

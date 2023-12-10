namespace Ams.Dto
{
    public class PayableReportDto
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public string PayableLedger { get; set; }
        public int ledger_id { get; set; }
        public string remarks { get; set; }
        public int user_id { get; set; }
        public string ledger_name { get; set; }

    }
}

namespace Ams.Dto
{
    public class ReceivableReportDto
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public string ReceivableLedger { get; set; }
        public int ledger_id { get; set; }
        public string remarks { get; set; }
        public string ledger_name { get; set; }
        public int user_id { get; set; }
    }
}

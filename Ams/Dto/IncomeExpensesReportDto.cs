namespace Ams.Dto
{
    public class IncomeExpensesReportDto
    {
        public DateTime date {  get; set; }
        public string IncomeLedger { get; set; }
        public string ExpensesLedger { get; set; }
        public decimal amount { get; set; }
        public string Ledger_name { get; set; }
        public decimal code { get; set; }
        public string remarks { get; set; }
        public int Id { get; set; }
        public decimal current { get; set; }    
    }
}

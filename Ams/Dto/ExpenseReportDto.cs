namespace Ams.Dto
{
    public class ExpenseReportDto
    {

        public DateTime date { get; set; }
        public string ExpensesLedger { get; set; }
        public decimal amount { get; set; }
        public string Ledger_name { get; set; }
        public decimal code { get; set; }
        public int Id { get; set; }
        public string remarks { get; set; }

    }
}

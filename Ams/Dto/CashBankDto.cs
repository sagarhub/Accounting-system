namespace Ams.Dto
{
    public class CashBankDto
    {
   
        public  decimal remaining_cash { get; set; }
        public decimal remaining_bank { get; set; }
   
        public string ledger { get; set; }
        public DateTime date { get; set; }
        public decimal dr_amount { get; set; }
        public decimal cr_amount { get; set; }
        public string type { get; set; }



    }
}

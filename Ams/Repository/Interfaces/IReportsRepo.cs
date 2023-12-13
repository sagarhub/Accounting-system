using Ams.Dto;
namespace Ams.Repository.Interfaces
{
    public interface IReportsRepo
    {
        Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync();
        Task<List<ExpenseReportDto>>GetExpenseReportsAsync();
        Task<List<PayableReportDto>> GetPayableReportsAsync();
        Task<List<ReceivableReportDto>> GetReceivableReportsAsync();
        Task<List<CashBankDto>> GetCashBanksAsync();
        Task<List<CashBankDto>> GetBanksAsync();
        Task<List<PaymentReportDto>> GetPaymentReportsAsync();
        Task<List<IncomeExpensesReportDto>> GetIncomesAsync();

        Task<List<IncomeExpensesReportDto>> GetexpesesAsync();
        Task<List<ReceiptReportDto>> GetReceiptReportAsync();

        Task<List<PayableReportDto>> GetRemainingPayableAsync();

        Task<List<ReceivableReportDto>> GetRemainingReceivableAsync();

    }
}

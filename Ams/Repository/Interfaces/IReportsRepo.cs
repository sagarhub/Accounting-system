using Ams.Dto;
namespace Ams.Repository.Interfaces
{
    public interface IReportsRepo
    {
        Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<ExpenseReportDto>>GetExpenseReportsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<PayableReportDto>> GetPayableReportsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<ReceivableReportDto>> GetReceivableReportsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<CashBankDto>> GetCashBanksAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<CashBankDto>> GetBanksAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<PaymentReportDto>> GetPaymentReportsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<IncomeExpensesReportDto>> GetIncomesAsync(DateTime? fromDate, DateTime? toDate);

        Task<List<IncomeExpensesReportDto>> GetexpesesAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<ReceiptReportDto>> GetReceiptReportAsync(DateTime? fromDate, DateTime? toDate);

        Task<List<PayableReportDto>> GetRemainingPayableAsync(DateTime? fromDate, DateTime? toDate);

        Task<List<ReceivableReportDto>> GetRemainingReceivableAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<CashBankDto>> GetCashStatementAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<CashBankDto>> GetBankStatementAsync(DateTime? fromDate, DateTime? toDate);
     Task<List<ExpenseReportDto>> GetCurrentExpenses();
        Task<List<IncomeExpensesReportDto>> GetCurrentIncome();
        Task<List<PaymentReportDto>> GetCurrentPayment();
        Task<List<ReceiptReportDto>> GetCurrentReceipt();
        Task<List<CashBankDto>> GetRemCashAsync();
        Task<List<GraphicsDto>> GetExpGraphAsync();
        Task<List<GraphicsDto>> GetTotalIncomeAsync();
        Task<List<GraphicsDto>> GetTotalExpensesAsync();
        Task<List<GraphicsDto>> GetIncGraphAsync();
        Task<List<IncomeExpensesReportDto>> GetTotalIAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<IncomeExpensesReportDto>> GetTotalEAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<IncomeExpensesReportDto>> GetTotalPayAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<IncomeExpensesReportDto>> GetTotalRecAsync(DateTime? fromDate, DateTime? toDate);
      



    }
}

using Ams.Dto;
namespace Ams.Repository.Interfaces
{
    public interface IReportsRepo
    {
        Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync();
        Task<List<ExpenseReportDto>>GetExpenseReportsAsync();
        Task<List<PayableReportDto>> GetPayableReportsAsync();
        Task<List<ReceivableReportDto>> GetReceivableReportsAsync();

    }
}

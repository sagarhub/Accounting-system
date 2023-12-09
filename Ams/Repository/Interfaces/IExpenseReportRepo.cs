using Ams.Dto;

namespace Ams.Repository.Interfaces
{
    public interface IExpenseReportRepo
    {
        Task<List<ExpenseReportDto>> GetExpenseReportsAsync();
    }
}
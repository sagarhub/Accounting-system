using Ams.Dto;
namespace Ams.Repository.Interfaces
{
    public interface IIncomeExpensesReportRepo
    {
        Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync();

    }
}

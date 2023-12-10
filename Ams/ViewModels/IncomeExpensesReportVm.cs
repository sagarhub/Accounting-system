using Ams.Dto;

namespace Ams.ViewModels
{
    public class IncomeExpensesReportVm
    {
        public List<IncomeExpensesReportDto> IncomeExpensesReports { get; set; }
        public List<ExpenseReportDto> ExpenseReports { get; set; }
        public List <PayableReportDto> PayableReports { get; set; }
        public List<ReceivableReportDto> ReceivableReports { get; set;}
        }
}

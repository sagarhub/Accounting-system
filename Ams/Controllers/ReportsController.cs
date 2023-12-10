using Ams.Repository.Interfaces;
using Ams.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ams.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportsRepo _reportsRepo;
         public ReportsController(IReportsRepo reportsRepo)
        {
           _reportsRepo = reportsRepo;
        }
         
    
        public async Task<IActionResult> IncomeExpenses()
        {

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(),
                ExpenseReports = await _reportsRepo.GetExpenseReportsAsync(),
            };
            return View(vm);
        }
        public async Task<IActionResult> IncomeReport()
        {

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(),
                ExpenseReports = await _reportsRepo.GetExpenseReportsAsync(),
            };
            return View(vm);
        }
        public async Task<IActionResult> ExpensesReport()
        {

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(),
                ExpenseReports = await _reportsRepo.GetExpenseReportsAsync(),
            };
            return View(vm);
        }
        public async Task<IActionResult> PayableReport()
        {
            var vm = new IncomeExpensesReportVm
            {
                PayableReports = await _reportsRepo.GetPayableReportsAsync(),

            };
            return View(vm);
        }
        public async Task<IActionResult> ReceivableReport()
        {
            var vm = new IncomeExpensesReportVm
            {
                ReceivableReports = await _reportsRepo.GetReceivableReportsAsync(),
            };
            return View(vm);

        }
    }
}

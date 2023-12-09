using Ams.Repository.Interfaces;
using Ams.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ams.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IIncomeExpensesReportRepo _incomeExpensesReportRepo;
         public ReportsController(IIncomeExpensesReportRepo incomeExpensesReportRepo)
        {
           _incomeExpensesReportRepo = incomeExpensesReportRepo;
        }
         
    
        public async Task<IActionResult> IncomeExpenses()
        {

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _incomeExpensesReportRepo.GetIncomeExpensesReportsAsync(),
            };
            return View(vm);
        }
    }
}

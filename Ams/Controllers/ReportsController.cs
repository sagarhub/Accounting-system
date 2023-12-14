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
                CashBanks = await _reportsRepo.GetCashBanksAsync(),
                cashBanks = await _reportsRepo.GetBanksAsync(),
                
            };
            return View(vm);
        }
        public async Task<IActionResult> IncomeReport(DateTime? fromDate, DateTime? toDate)
        {

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(),
                incomes = await _reportsRepo.GetIncomesAsync(fromDate, toDate),
            };
            return View(vm);
        }
        public async Task<IActionResult> ExpensesReport(DateTime? fromDate, DateTime? toDate)
        {

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(),
                expenses = await _reportsRepo.GetexpesesAsync(fromDate, toDate),    
                
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
        public async Task<IActionResult> PaymentReport()
        {
            var vm = new IncomeExpensesReportVm
            {
                PaymentReports = await _reportsRepo.GetPaymentReportsAsync(),
            };
            return View(vm);
        }
        public async Task<IActionResult> ReceiptReport()
        {
            var vm = new IncomeExpensesReportVm
            {
                receiptReports = await _reportsRepo.GetReceiptReportAsync(),
            };
            return View(vm);
        }
        public async Task<IActionResult> PayableReceivable()
        {
            var vm = new IncomeExpensesReportVm
            {
                payableReports = await _reportsRepo.GetRemainingPayableAsync(),
                receivableReports = await _reportsRepo.GetRemainingReceivableAsync(),
            };
            return View(vm);
        }

        public async Task<IActionResult> CashStatement()
        {
            var vm = new IncomeExpensesReportVm
            {
                CashStatement = await _reportsRepo.GetCashStatementAsync(),
            };
            return View(vm);

        }

        public async Task<IActionResult> Bankstatement()
        {
            var vm = new IncomeExpensesReportVm
            {
                BankStatement = await _reportsRepo.GetBankStatementAsync(),
            };
            return View(vm);
        }
        

    }
}

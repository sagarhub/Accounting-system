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
         
    
        public async Task<IActionResult> IncomeExpenses(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(fromDate,  toDate),
                ExpenseReports = await _reportsRepo.GetExpenseReportsAsync(fromDate, toDate),
                CashBanks = await _reportsRepo.GetCashBanksAsync( fromDate,toDate),
                cashBanks = await _reportsRepo.GetBanksAsync(fromDate, toDate),
                Tincome = await _reportsRepo.GetTotalIAsync(fromDate, toDate),
                Texpenses = await _reportsRepo.GetTotalEAsync(fromDate,toDate),
              

            };
            return View(vm);
        }
        public async Task<IActionResult> IncomeReport(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(fromDate, toDate),
                incomes = await _reportsRepo.GetIncomesAsync(fromDate, toDate),
            };
            return View(vm);
        }
        public async Task<IActionResult> ExpensesReport(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;

            var vm = new IncomeExpensesReportVm
            {
                IncomeExpensesReports = await _reportsRepo.GetIncomeExpensesReportsAsync(fromDate, toDate),
                expenses = await _reportsRepo.GetexpesesAsync(fromDate, toDate),    
                
            };
            return View(vm);
        }
        public async Task<IActionResult> PayableReport(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                PayableReports = await _reportsRepo.GetPayableReportsAsync(fromDate, toDate),

            };
            return View(vm);
        }
        public async Task<IActionResult> ReceivableReport(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                ReceivableReports = await _reportsRepo.GetReceivableReportsAsync(fromDate, toDate),
            };
            return View(vm);

        }
        public async Task<IActionResult> PaymentReport(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                PaymentReports = await _reportsRepo.GetPaymentReportsAsync(fromDate, toDate),
            };
            return View(vm);
        }
        public async Task<IActionResult> ReceiptReport(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                receiptReports = await _reportsRepo.GetReceiptReportAsync(fromDate, toDate),
            };
            return View(vm);
        }
        public async Task<IActionResult> PayableReceivable(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                payableReports = await _reportsRepo.GetRemainingPayableAsync(fromDate, toDate),
                receivableReports = await _reportsRepo.GetRemainingReceivableAsync(fromDate, toDate),
                Tpayable = await _reportsRepo.GetTotalPayAsync(fromDate, toDate),
                Treceivable = await _reportsRepo.GetTotalRecAsync(fromDate, toDate),
            };
            return View(vm);
        }

        public async Task<IActionResult> CashStatement(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                CashStatement = await _reportsRepo.GetCashStatementAsync(fromDate, toDate),
            };
            return View(vm);

        }

        public async Task<IActionResult> Bankstatement(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today;
            var vm = new IncomeExpensesReportVm
            {
                BankStatement = await _reportsRepo.GetBankStatementAsync(fromDate, toDate),
            };
            return View(vm);
        }
        public async Task<IActionResult> LedgerReport()
        {
            var vm = new IncomeExpensesReportVm
            {
                LedgerReport = await _reportsRepo.GetLedegrsAsync()
            };
            return View(vm);
        }
        
        

    }
}

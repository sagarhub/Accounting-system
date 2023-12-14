using Ams.Models;
using Ams.Repository.Interfaces;
using Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ams.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportsRepo _reportsRepo;

        public HomeController(ILogger<HomeController> logger, IReportsRepo reportsRepo)
        {
            _logger = logger;
            _reportsRepo = reportsRepo;

        }
      
        public async Task<IActionResult> Index()
        {
            var vm = new IncomeExpensesReportVm
            {
                CurrentExp = await _reportsRepo.GetCurrentExpenses(),
                currentInc = await _reportsRepo.GetCurrentIncome(),
                currentpmt = await _reportsRepo.GetCurrentPayment(),
                currentrcpt = await _reportsRepo.GetCurrentReceipt(),
                currentCash = await _reportsRepo.GetRemCashAsync(),
                
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
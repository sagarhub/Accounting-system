﻿using Ams.Dto;

namespace Ams.ViewModels
{
    public class IncomeExpensesReportVm
    {
        public List<IncomeExpensesReportDto> IncomeExpensesReports { get; set; }
        public List<ExpenseReportDto> ExpenseReports { get; set; }
        public List<PayableReportDto> PayableReports { get; set; }
        public List<ReceivableReportDto> ReceivableReports { get; set; }

        public List<CashBankDto> CashBanks { get; set; }
        public List<CashBankDto> cashBanks { get; set; }

        public List<PaymentReportDto> PaymentReports { get; set; }

        public List<IncomeExpensesReportDto> incomes { get; set; }

        public List<IncomeExpensesReportDto> expenses { get; set; }
        public List<ReceiptReportDto> receiptReports { get; set; }

        public List<PayableReportDto> payableReports { get; set; }

        public List<ReceivableReportDto> receivableReports { get; set; }

        public List<CashBankDto> CashStatement { get; set; }

        public List<CashBankDto> BankStatement { get; set; }

        public List<ExpenseReportDto> CurrentExp {get; set;}

        public List<IncomeExpensesReportDto> currentInc { get; set;}

        public List<PaymentReportDto> currentpmt { get; set; }
        public List<ReceiptReportDto> currentrcpt { get; set; }
        public List<CashBankDto> currentCash { get; set; }  
        public List<GraphicsDto> ExpGraph { get; set; }
        public List<GraphicsDto> TotalIncome { get; set; }
        public List<GraphicsDto> TotalExpenses { get; set; }
        public List<GraphicsDto> IncGraph { get; set; }

        public List<IncomeExpensesReportDto> Tincome { get; set; }
        public List<IncomeExpensesReportDto> Texpenses { get; set; }
        public List<IncomeExpensesReportDto> Tpayable { get; set; }
        public List<IncomeExpensesReportDto> Treceivable { get; set; }
        public List<LedgerDto> LedgerReport { get; set; }
        }
}

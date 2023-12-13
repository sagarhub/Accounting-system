using Ams.Dto;
using Ams.Provider.Interfaces;
using Ams.Repository.Interfaces;
using Dapper;

namespace Ams.Repository
{
    public class ReportsRepo : IReportsRepo
    {
        private readonly IDbConnectionProvider connectionProvider;

        public ReportsRepo(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public async Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var IncomeQuery = @"SELECT l.""Ledger_name"" ,sum(i.""amount"")as amount,l.""code"" 
FROM income i join ""Ledgers"" l on l.""Id"" =i.""IncomeLedger"" group by (l.""Ledger_name"",l.code)  ";

            return (await conn.QueryAsync<IncomeExpensesReportDto>(IncomeQuery)).ToList();
        }
        public async Task<List<ExpenseReportDto>> GetExpenseReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ExpensesQuery = @"select  l.""Ledger_name""  ,sum(e.amount) as amount from ""Expenses"" e join ""Ledgers"" l on l.""Id"" = e.""ExpensesLedger"" group by (l.""Ledger_name"") ";
            return (await conn.QueryAsync<ExpenseReportDto>(ExpensesQuery)).ToList();
        }
        public async Task<List<ReceivableReportDto>> GetReceivableReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ReceivableQuery = @"
select r.""date"" ,r.amount ,l.""Ledger_name"" ,l2.""Ledger_name"" as ""ReceivableLedger"" ,r.remarks  from receivables r join ""Ledgers"" l on r.ledger_id =l.""Id"" 
join ""Ledgers"" l2 on r.""ReceivableLedger"" =l2.""Id"" ";
            return (await conn.QueryAsync<ReceivableReportDto>(ReceivableQuery)).ToList();
        }
        public async Task<List<PayableReportDto>> GetPayableReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var PayableQuery = @"select p.date,p.amount,l2.""Ledger_name""  as ""PayableLedger"",l.""Ledger_name""  ,p.""remarks""  from paybles  p join ""Ledgers"" l on p.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on  p.""PayableLedger"" = l2.""Id"" ";
            return (await conn.QueryAsync<PayableReportDto>(PayableQuery)).ToList();


        }
        public async Task<List<CashBankDto>> GetCashBanksAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var CashBankQuery = @"    SELECT
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE 0 END), 0) AS total_income,
    COALESCE(SUM(CASE WHEN type IN (2, 4, 5) THEN amount ELSE 0 END), 0) AS total_expenses,
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE -amount END), 0) AS remaining_cash

FROM
    transactions t join ""Ledgers"" l on t.dr_ledger  = l.""Id"" join ""Ledgers"" l2 on t.cr_ledger =l2.""Id"" 
    where (l.""Parent_ledgerId"" =0 or l2.""Parent_ledgerId"" =0) and (l.""BankId"" =0 and l2.""BankId"" =0)";
            return (await conn.QueryAsync<CashBankDto>(CashBankQuery)).ToList();
        }

        public async Task<List<CashBankDto>> GetBanksAsync()
        {

            using var conn = connectionProvider.GetConnection();
            var BankQuery = @"    SELECT
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE 0 END), 0) AS total_income,
    COALESCE(SUM(CASE WHEN type IN (2, 4, 5) THEN amount ELSE 0 END), 0) AS total_expenses,
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE -amount END), 0) AS remaining_cash

FROM
    transactions t join ""Ledgers"" l on t.dr_ledger  = l.""Id"" join ""Ledgers"" l2 on t.cr_ledger =l2.""Id"" 
    where (l.""Parent_ledgerId"" =0 or l2.""Parent_ledgerId"" =0) and (l.""BankId"" !=0 or l2.""BankId"" !=0)";
            return (await conn.QueryAsync<CashBankDto>(BankQuery)).ToList();
        }

        public async Task<List<PaymentReportDto>> GetPaymentReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var PaymentQuery = @"select p.date,p.amount,l2.""Ledger_name"" as ""PayableLedger"",l.""Ledger_name"",p.""remarks""   from ""Payment"" p join ""Ledgers"" l on p.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on  p.""PayableLedger"" = l2.""Id"" ";
            return(await conn.QueryAsync<PaymentReportDto>(PaymentQuery)).ToList();
        }

        public async Task<List<IncomeExpensesReportDto>> GetIncomesAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var IncQuery = @"select i.""date"" ,i.amount ,l.""Ledger_name"" ,l2.""Ledger_name"" as IncomeLedger ,i.remarks  from income i join ""Ledgers"" l on i.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on i.""IncomeLedger"" = l2.""Id"" ";
            return(await conn.QueryAsync<IncomeExpensesReportDto>(IncQuery)).ToList();
        }
         public async Task<List<IncomeExpensesReportDto>> GetexpesesAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ExpQuery = @"select e.""date"" ,e.amount ,l.""Ledger_name"" ,l2.""Ledger_name"" as ExpensesLedger , e.remarks from ""Expenses"" e join ""Ledgers"" l on e.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on e.""ExpensesLedger"" =l2.""Id"" ";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(ExpQuery)).ToList();
        }

        public async Task<List<ReceiptReportDto>> GetReceiptReportAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ReceiptQuery = @"select r.date,r.amount,l2.""Ledger_name""  as ReceivableLedger,l.""Ledger_name"" ,r.""remarks""  from ""Receipt"" r join ""Ledgers"" l on r.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on  r.""ReceivableLedger"" = l2.""Id"" ";
            return (await conn.QueryAsync<ReceiptReportDto>(ReceiptQuery)).ToList();
        }
        public async Task<List<PayableReportDto>> GetRemainingPayableAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var RemainingPayableQuery = @" select p.""PayableLedger"" ,l.""Ledger_name"" ,
   p.amount -coalesce (sum(p2.amount),0) as remaining
    from paybles p join ""Payment"" p2 on p.""PayableLedger"" =p2.""PayableLedger""
    join ""Ledgers"" l on p.""PayableLedger"" = l.""Id"" 
    group by(p.""PayableLedger"",p.amount,l.""Ledger_name"")";
            return(await conn.QueryAsync<PayableReportDto>(RemainingPayableQuery)).ToList();
        }
        public async Task<List<ReceivableReportDto>> GetRemainingReceivableAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ReminingReceivableQuery = @"select r.""ReceivableLedger"" ,l.""Ledger_name"" ,r.amount -coalesce (sum(r2.amount),0) as remaining
    from receivables r join ""Receipt"" r2 on r.""ReceivableLedger"" =r2.""ReceivableLedger"" 
    join ""Ledgers"" l on r.""ReceivableLedger"" = l.""Id""
    group by(r.""ReceivableLedger"",r.amount,l.""Ledger_name"")";
            return (await conn.QueryAsync<ReceivableReportDto>(ReminingReceivableQuery)).ToList();   
        }
   


}
}

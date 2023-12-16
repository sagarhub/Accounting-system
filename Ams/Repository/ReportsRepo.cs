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

        public async Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var IncomeQuery = @"SELECT l.""Ledger_name"" ,sum(i.""amount"")as amount,l.""code"" 
FROM income i join ""Ledgers"" l on l.""Id"" =i.""IncomeLedger"" where i.""date"" between @FromDate and @ToDate group by (l.""Ledger_name"",l.code)";

            return (await conn.QueryAsync<IncomeExpensesReportDto>(IncomeQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<ExpenseReportDto>> GetExpenseReportsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var ExpensesQuery = @"select  l.""Ledger_name""  ,sum(e.amount) as amount
from ""Expenses"" e join ""Ledgers"" l on l.""Id"" = e.""ExpensesLedger"" where e.date between @FromDate and @ToDate group by (l.""Ledger_name"") ";
            return (await conn.QueryAsync<ExpenseReportDto>(ExpensesQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<ReceivableReportDto>> GetReceivableReportsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var ReceivableQuery = @"
select r.""date"" ,r.amount ,l.""Ledger_name"" ,l2.""Ledger_name"" as ""ReceivableLedger"" ,r.remarks  from receivables r join ""Ledgers"" l on r.ledger_id =l.""Id"" 
join ""Ledgers"" l2 on r.""ReceivableLedger"" =l2.""Id"" where r.date between @FromDate and @ToDate";
            return (await conn.QueryAsync<ReceivableReportDto>(ReceivableQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<PayableReportDto>> GetPayableReportsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var PayableQuery = @"select p.date,p.amount,l2.""Ledger_name""  as ""PayableLedger"",l.""Ledger_name""  ,p.""remarks""  from paybles  p join ""Ledgers"" l on p.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on  p.""PayableLedger"" = l2.""Id"" where p.date between @FromDate and @ToDate";
            return (await conn.QueryAsync<PayableReportDto>(PayableQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();


        }
        public async Task<List<CashBankDto>> GetCashBanksAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var CashBankQuery = @"   select
	 COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE 0 END), 0) AS total_income,
    COALESCE(SUM(CASE WHEN type IN (2, 4, 5) THEN amount ELSE 0 END), 0) AS total_expenses,
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE -amount END), 0) AS remaining_cash

FROM
    transactions t join ""Ledgers"" l on t.dr_ledger  = l.""Id"" join ""Ledgers"" l2 on t.cr_ledger =l2.""Id""
    where (l.""Parent_ledgerId"" =0 or l2.""Parent_ledgerId"" =0) and (l.""BankId"" =0 and l2.""BankId"" =0) 
    and t.transaction_date between @FromDate and @ToDate ";
            return (await conn.QueryAsync<CashBankDto>(CashBankQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            }  
                )).ToList();
        }

        public async Task<List<CashBankDto>> GetBanksAsync(DateTime? fromDate, DateTime? toDate)
        {

            using var conn = connectionProvider.GetConnection();
            var BankQuery = @"SELECT
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE 0 END), 0) AS total_income,
    COALESCE(SUM(CASE WHEN type IN (2, 4, 5) THEN amount ELSE 0 END), 0) AS total_expenses,
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE -amount END), 0) AS remaining_bank

FROM
    transactions t join ""Ledgers"" l on t.dr_ledger  = l.""Id"" join ""Ledgers"" l2 on t.cr_ledger =l2.""Id"" 
    where (l.""Parent_ledgerId"" =0 or l2.""Parent_ledgerId"" =0) and (l.""BankId"" !=0 or l2.""BankId"" !=0)
and t.transaction_date between @FromDate and @ToDate";
            return (await conn.QueryAsync<CashBankDto>(BankQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }

        public async Task<List<PaymentReportDto>> GetPaymentReportsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var PaymentQuery = @"select p.date,p.amount,l2.""Ledger_name"" as ""PayableLedger"",l.""Ledger_name"",p.""remarks""   from ""Payment"" p join ""Ledgers"" l on p.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on  p.""PayableLedger"" = l2.""Id"" where p.date between @FromDate and @ToDate ";
            return(await conn.QueryAsync<PaymentReportDto>(PaymentQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }

        public async Task<List<IncomeExpensesReportDto>> GetIncomesAsync(DateTime? fromDate,DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var IncQuery = @"select cast(i.""date"" as ""date"" ),i.amount ,l.""Ledger_name"" ,l2.""Ledger_name"" as IncomeLedger ,i.remarks  from income i join ""Ledgers"" l on i.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on i.""IncomeLedger"" = l2.""Id""
where ""date"" between @FromDate and @ToDate ";
            return(await conn.QueryAsync<IncomeExpensesReportDto>(IncQuery,new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
         public async Task<List<IncomeExpensesReportDto>> GetexpesesAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var ExpQuery = @"select cast(e.""date"" as ""date"" ) ,e.amount ,l.""Ledger_name"" ,l2.""Ledger_name"" as ExpensesLedger , e.remarks from ""Expenses"" e join ""Ledgers"" l on e.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on e.""ExpensesLedger"" =l2.""Id"" where ""date"" between @FromDate and @ToDate";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(ExpQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            }
                )).ToList();
        }

        public async Task<List<ReceiptReportDto>> GetReceiptReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var ReceiptQuery = @"select r.date,r.amount,l2.""Ledger_name""  as ReceivableLedger,l.""Ledger_name"" ,r.""remarks""  from ""Receipt"" r join ""Ledgers"" l on r.ledger_id = l.""Id"" 
join ""Ledgers"" l2 on  r.""ReceivableLedger"" = l2.""Id"" where r.date between @FromDate and @ToDate ";
            return (await conn.QueryAsync<ReceiptReportDto>(ReceiptQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<PayableReportDto>> GetRemainingPayableAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var RemainingPayableQuery = @" select p.""PayableLedger"" ,l.""Ledger_name"" ,
   p.amount -coalesce (sum(p2.amount),0) as remaining
    from paybles p left join ""Payment"" p2 on p.""PayableLedger"" =p2.""PayableLedger""
    join ""Ledgers"" l on p.""PayableLedger"" = l.""Id"" where p.date between @FromDate and @ToDate
    group by(p.""PayableLedger"",p.amount,l.""Ledger_name"")";
            return(await conn.QueryAsync<PayableReportDto>(RemainingPayableQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<ReceivableReportDto>> GetRemainingReceivableAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var ReminingReceivableQuery = @"select r.""ReceivableLedger"" ,l.""Ledger_name"" ,r.amount -coalesce (sum(r2.amount),0) as remaining
    from receivables r left join ""Receipt"" r2 on r.""ReceivableLedger"" =r2.""ReceivableLedger"" 
    join ""Ledgers"" l on r.""ReceivableLedger"" = l.""Id"" where r.date between @FromDate and @ToDate
    group by(r.""ReceivableLedger"",r.amount,l.""Ledger_name"")";
            return (await conn.QueryAsync<ReceivableReportDto>(ReminingReceivableQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();   
        }
        public async Task<List<CashBankDto>> GetCashStatementAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var CashStatementQuery = @"SELECT
    t.transaction_date as t_date,
    concat( l2.""Ledger_name"",' - ' ,l.""Ledger_name"") as ledger,
    SUM(CASE WHEN t.""type"" IN (1, 4, 6) THEN t.amount ELSE 0 END) AS dr_amount,
    SUM(CASE WHEN t.""type"" IN (2, 3, 5) THEN t.amount ELSE 0 END) AS cr_amount,
    tt.""Name"" as type
FROM
    transactions t
JOIN
    ""Ledgers"" l ON t.dr_ledger = l.""Id""
JOIN
    ""Ledgers"" l2 ON t.cr_ledger = l2.""Id""
join ""txnTypes"" tt on t.""type"" =tt.""Id"" 
WHERE
    (l.""Parent_ledgerId"" = 0 OR l2.""Parent_ledgerId"" = 0)
    AND (l.""BankId"" = 0 AND l2.""BankId"" = 0) and t.transaction_date between @FromDate and @ToDate
GROUP BY
    t.transaction_date,
    l.""Ledger_name"",
    tt.""Name"" ,
    l2.""Ledger_name""";
            return (await conn.QueryAsync<CashBankDto>(CashStatementQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }

        public async Task<List<CashBankDto>> GetBankStatementAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var BankStatementQuery = @"SELECT
    t.transaction_date as t_date,
    concat( l2.""Ledger_name"",' - ' ,l.""Ledger_name"") as ledger,
    SUM(CASE WHEN t.""type"" IN (1, 4, 6) THEN t.amount ELSE 0 END) AS dr_amount,
    SUM(CASE WHEN t.""type"" IN (2, 3, 5) THEN t.amount ELSE 0 END) AS cr_amount,
    tt.""Name"" as type
FROM
    transactions t
JOIN
    ""Ledgers"" l ON t.dr_ledger = l.""Id""
JOIN
    ""Ledgers"" l2 ON t.cr_ledger = l2.""Id""
join ""txnTypes"" tt on t.""type"" =tt.""Id"" 
WHERE
    (l.""Parent_ledgerId"" = 0 OR l2.""Parent_ledgerId"" = 0)
    AND (l.""BankId"" != 0 or l2.""BankId"" != 0) and t.transaction_date between @FromDate and @ToDate
GROUP BY
    t.transaction_date,
    l.""Ledger_name"",
    tt.""Name"" ,
    l2.""Ledger_name""";
            return (await conn.QueryAsync<CashBankDto>(BankStatementQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }

        public async Task<List<ExpenseReportDto>> GetCurrentExpenses()
        {
            using var conn = connectionProvider.GetConnection();
            var CurrentExp = @"select sum(amount) as current from ""Expenses"" e where ""date"" ::""date"" = current_date ";
            return (await conn.QueryAsync<ExpenseReportDto>(CurrentExp)).ToList();
        }
        public async Task<List<IncomeExpensesReportDto>> GetCurrentIncome()
        {
            using var conn = connectionProvider.GetConnection();
            var CurrentInc = @"select sum(amount) as current from income i  where ""date"" ::""date"" = current_date  ";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(CurrentInc)).ToList();
        }
        public async Task<List<PaymentReportDto>> GetCurrentPayment()
        {
            using var conn = connectionProvider.GetConnection();
            var currentPmt = @"select sum(amount) as current from ""Payment"" p  where ""date"" ::""date"" = current_date  	";
            return (await conn.QueryAsync<PaymentReportDto>(currentPmt)).ToList();  
        }
        public async Task<List<ReceiptReportDto>> GetCurrentReceipt()
        {
            using var conn = connectionProvider.GetConnection();
            var currentRecpt = @"select sum(amount) as current from ""Receipt"" r  where ""date"" ::""date"" = current_date  	";
            return (await conn.QueryAsync<ReceiptReportDto>(currentRecpt)).ToList();
        }
        public async Task<List<CashBankDto>> GetRemCashAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var Remcash = @"   select
	 COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE 0 END), 0) AS total_income,
    COALESCE(SUM(CASE WHEN type IN (2, 4, 5) THEN amount ELSE 0 END), 0) AS total_expenses,
    COALESCE(SUM(CASE WHEN type IN (1, 3, 6) THEN amount ELSE -amount END), 0) AS remaining_cash

FROM
    transactions t join ""Ledgers"" l on t.dr_ledger  = l.""Id"" join ""Ledgers"" l2 on t.cr_ledger =l2.""Id""
    where (l.""Parent_ledgerId"" =0 or l2.""Parent_ledgerId"" =0) and (l.""BankId"" =0 and l2.""BankId"" =0)";
            return (await conn.QueryAsync<CashBankDto>(Remcash)).ToList();
        }

        public async Task<List<GraphicsDto>> GetExpGraphAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ExpGraphQuery = @"SELECT
    EXTRACT(YEAR FROM date) AS year,
    EXTRACT(MONTH FROM date) AS month,
    CASE 
        WHEN EXTRACT(MONTH FROM date) = 1 THEN 'January'
        WHEN EXTRACT(MONTH FROM date) = 2 THEN 'February'
        WHEN EXTRACT(MONTH FROM date) = 3 THEN 'March'
        WHEN EXTRACT(MONTH FROM date) = 4 THEN 'April'
        WHEN EXTRACT(MONTH FROM date) = 5 THEN 'May'
        WHEN EXTRACT(MONTH FROM date) = 6 THEN 'June'
        WHEN EXTRACT(MONTH FROM date) = 7 THEN 'July'
        WHEN EXTRACT(MONTH FROM date) = 8 THEN 'August'
        WHEN EXTRACT(MONTH FROM date) = 9 THEN 'September'
        WHEN EXTRACT(MONTH FROM date) = 10 THEN 'October'
        WHEN EXTRACT(MONTH FROM date) = 11 THEN 'November'
        WHEN EXTRACT(MONTH FROM date) = 12 THEN 'December'
    END AS month_name,
    SUM(amount) AS amount
FROM
    ""Expenses"" e 
GROUP BY
    EXTRACT(YEAR FROM date),
    EXTRACT(MONTH FROM date)
ORDER BY
    EXTRACT(YEAR FROM date), EXTRACT(MONTH FROM date)";
            return (await conn.QueryAsync<GraphicsDto>(ExpGraphQuery)).ToList();
        }
        public async Task<List<GraphicsDto>> GetTotalIncomeAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var TotalIncomeQuery = @"    select sum(amount) as total_income from income i ";
            return(await conn.QueryAsync<GraphicsDto>(TotalIncomeQuery)).ToList();
        }
        public async Task<List<GraphicsDto>> GetTotalExpensesAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var TotalExpensesQuery = @"    select sum(amount) as total_expenses from ""Expenses"" e ";
            return (await conn.QueryAsync<GraphicsDto>(TotalExpensesQuery)).ToList();
        }
        public async Task<List<GraphicsDto>> GetIncGraphAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var IncGraphQuery = @"SELECT
    EXTRACT(YEAR FROM date) AS year,
    EXTRACT(MONTH FROM date) AS month,
    CASE 
        WHEN EXTRACT(MONTH FROM date) = 1 THEN 'January'
        WHEN EXTRACT(MONTH FROM date) = 2 THEN 'February'
        WHEN EXTRACT(MONTH FROM date) = 3 THEN 'March'
        WHEN EXTRACT(MONTH FROM date) = 4 THEN 'April'
        WHEN EXTRACT(MONTH FROM date) = 5 THEN 'May'
        WHEN EXTRACT(MONTH FROM date) = 6 THEN 'June'
        WHEN EXTRACT(MONTH FROM date) = 7 THEN 'July'
        WHEN EXTRACT(MONTH FROM date) = 8 THEN 'August'
        WHEN EXTRACT(MONTH FROM date) = 9 THEN 'September'
        WHEN EXTRACT(MONTH FROM date) = 10 THEN 'October'
        WHEN EXTRACT(MONTH FROM date) = 11 THEN 'November'
        WHEN EXTRACT(MONTH FROM date) = 12 THEN 'December'
    END AS month_name,
    SUM(amount) AS amount
FROM
    ""income"" i
GROUP BY
    EXTRACT(YEAR FROM date),
    EXTRACT(MONTH FROM date)
ORDER BY
    EXTRACT(YEAR FROM date), EXTRACT(MONTH FROM date)";
            return (await conn.QueryAsync<GraphicsDto>(IncGraphQuery)).ToList();
        }

        public async Task<List<IncomeExpensesReportDto>> GetTotalIAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var TotalIncQuery = @"select sum(amount) as total_income from income i  where 
        date between @FromDate and @ToDate";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(TotalIncQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<IncomeExpensesReportDto>> GetTotalEAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var TotalExpQuery = @"select sum(amount) as total_expenses from ""Expenses"" e where date between @FromDate and @ToDate ";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(TotalExpQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<IncomeExpensesReportDto>> GetTotalPayAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var TotalpayQuery = @"select sum(amount) as total_payable from paybles  where date between @FromDate and @ToDate ";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(TotalpayQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }
        public async Task<List<IncomeExpensesReportDto>> GetTotalRecAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var conn = connectionProvider.GetConnection();
            var TotalRecQuery = @"select r.amount -coalesce (sum(r2.amount),0) as total_receivable
    from receivables r  join ""Receipt"" r2 on r.""ReceivableLedger"" =r2.""ReceivableLedger"" 
    where r.date between @FromDate and @ToDate
    group by(r.amount)";
            return (await conn.QueryAsync<IncomeExpensesReportDto>(TotalRecQuery, new
            {
                FromDate = fromDate,
                ToDate = toDate
            })).ToList();
        }

        public async Task<List<LedgerDto>> GetLedegrsAsync()
        {
            var conn = connectionProvider.GetConnection();
            var ledgerQuery = @"select l.""Ledger_name"" as ledger_name ,l.code ,pg.""name"" parent_name ,l.""Description""   from ""Ledgers"" l 
join ""ParentGroups"" pg on l.""Parent_ledgerId"" =pg.id ";
            return (await conn.QueryAsync<LedgerDto>(ledgerQuery)).ToList();
        }
    }
}

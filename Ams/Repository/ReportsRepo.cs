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
            var IncomeQuery = @"SELECT i.""Id"",l.""Ledger_name"" ,sum(i.""amount"")as amount,l.code,i.""remarks""  FROM income i join ""Ledgers"" l on l.""Id"" =i.""IncomeLedger"" group by (l.""Ledger_name"",l.code,i.""Id"")  ";

            return (await conn.QueryAsync<IncomeExpensesReportDto>(IncomeQuery)).ToList();
        }
        public async Task<List<ExpenseReportDto>>GetExpenseReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var ExpensesQuery = @"select e.""Id"", l.""Ledger_name""  ,sum(e.amount) as amount,e.""remarks"" from ""Expenses"" e join ""Ledgers"" l on l.""Id"" = e.""ExpensesLedger"" group by (l.""Ledger_name"",l.code,e.""Id"") ";
            return (await conn.QueryAsync<ExpenseReportDto>(ExpensesQuery)).ToList();
        }
        public async Task<List<ReceivableReportDto>> GetReceivableReportsAsync() 
      {
            using var conn = connectionProvider.GetConnection();
            var ReceivableQuery = @"select r.""Id"" ,r.""date"" ,sum(r.amount) as amount,l2.""Ledger_name"" as ""ReceivableLedger""  ,r.remarks ,l.""Ledger_name"" ,l.code  from receivables r 

join ""Ledgers"" l on r.ledger_id = l.""Id""
join ""Ledgers"" l2 on r.""ReceivableLedger""  = l2.""Id"" 
group by (r.""Id"",l.code ,l.""Ledger_name"" ,l2.""Ledger_name"")";
            return (await conn.QueryAsync<ReceivableReportDto>(ReceivableQuery)).ToList();
        }
        public async Task<List<PayableReportDto>> GetPayableReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var PayableQuery = @"select p.""Id"" ,p.""date"" ,sum(p.amount) as amount,l2.""Ledger_name"" as ""PayableLedger""  ,p.remarks ,l.""Ledger_name"" ,l.code  from paybles p  
join ""Ledgers"" l on p.ledger_id = l.""Id""
join ""Ledgers"" l2 on p.""PayableLedger""  = l2.""Id"" 
group by (p.""Id"",l.code ,l.""Ledger_name"" ,l2.""Ledger_name"")";
                return (await conn.QueryAsync<PayableReportDto>(PayableQuery)).ToList();
       
         
        }
    }
}

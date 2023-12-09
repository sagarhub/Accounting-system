using Ams.Dto;
using Ams.Provider.Interfaces;
using Ams.Repository.Interfaces;
using Dapper;

namespace Ams.Repository
{
    public class IncomeExpensesReportRepo : IIncomeExpensesReportRepo
    {
        private readonly IDbConnectionProvider connectionProvider;

        public IncomeExpensesReportRepo(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public async Task<List<IncomeExpensesReportDto>> GetIncomeExpensesReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var query = @"SELECT l.""Ledger_name"" ,sum(i.""amount"")as amount,l.code  FROM income i join ""Ledgers"" l on l.""Id"" =i.""IncomeLedger"" group by (l.""Ledger_name"",l.code)  ";

            return (await conn.QueryAsync<IncomeExpensesReportDto>(query)).ToList();
        }
    }
}

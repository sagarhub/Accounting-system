using Ams.Dto;
using Ams.Provider.Interfaces;
using Ams.Repository.Interfaces;
using Dapper;

namespace Ams.Repository
{
    public class ExpenseReportRepo : IExpenseReportRepo
    {
        private readonly IDbConnectionProvider connectionProvider;

        public ExpenseReportRepo(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public async Task<List<ExpenseReportDto>> GetExpenseReportsAsync()
        {
            using var conn = connectionProvider.GetConnection();
            var query = @"SELECT amount  FROM income ";

            return (await conn.QueryAsync<ExpenseReportDto>(query)).ToList();
        }
    }
}

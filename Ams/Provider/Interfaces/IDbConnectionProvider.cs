using System.Data;

namespace Ams.Provider.Interfaces
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection();
    }
}
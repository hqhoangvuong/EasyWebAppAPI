using EasyWebApp.Data.Consts;
using EasyWebApp.Data.DbContext;

namespace EasyWebApp.Data.DbContextProvider
{
    public interface IDbContextProvider
    {
        CustomerDbContext GetApplicationDbContext(string connStr, AppConst.DbSqlTypes dbType);
    }
}

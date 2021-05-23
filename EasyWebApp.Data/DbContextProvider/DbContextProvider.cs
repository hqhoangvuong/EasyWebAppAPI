using EasyWebApp.Data.Consts;
using EasyWebApp.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace EasyWebApp.Data.DbContextProvider
{
    public class DbContextProvider : IDbContextProvider
    {
        private readonly CustomerDbContext _context;

        public DbContextProvider(CustomerDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }

        public CustomerDbContext GetApplicationDbContext(string connStr, AppConst.DbSqlTypes dbType)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
            if(connStr == null)
            {
                return _context;
            }

            switch (dbType)
            {
                case AppConst.DbSqlTypes.SQLServer:
                    optionsBuilder.UseSqlServer(connStr);
                    break;
                case AppConst.DbSqlTypes.MySQL:
                    optionsBuilder.UseMySql(connStr,
                                            new MySqlServerVersion(new Version(8, 0, 21)),
                                            mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend));
                    break;
                case AppConst.DbSqlTypes.PostgreSQL:
                    optionsBuilder.UseNpgsql(connStr);
                    break;
            }
            
            return new CustomerDbContext(optionsBuilder.Options);
        }
    }
}

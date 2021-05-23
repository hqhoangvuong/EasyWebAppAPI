using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasyWebApp.Data.Consts.AppConst;

namespace EasyWebApp.Data.Entities.ServiceWebEntities
{
    public class UserDbInfo : BaseEntity, IBaseEntity
    {
        public string Guid { get; set; }
        public string UserId { get; set; }
        public DbSqlTypes DbType { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InitialCatalog { get; set; }
        public string FrontEndId { get; set; }
        public string BackEndId { get; set; }

        public string BuildConnStr()
        {
            if(DbType != null)
            {
                switch (DbType)
                {
                    case DbSqlTypes.PostgreSQL:
                        NpgsqlConnectionStringBuilder postgresBuilder = new NpgsqlConnectionStringBuilder();
                        postgresBuilder.Host = Server;
                        postgresBuilder.Database = InitialCatalog;
                        postgresBuilder.Username = Username;
                        postgresBuilder.Password = Password;
                        return postgresBuilder.ConnectionString;
                    default:
                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                        builder.DataSource = this.Server;
                        builder.UserID = this.Username;
                        builder.Password = this.Password;
                        builder.InitialCatalog = this.InitialCatalog;
                        builder.ConnectTimeout = 10;
                        return builder.ConnectionString;
                }
            }

            return "";

        }
    }
}

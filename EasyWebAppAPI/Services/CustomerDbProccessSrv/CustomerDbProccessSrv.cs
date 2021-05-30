using AutoMapper;
using EasyWebApp.API.Services.CustomerDbConnStrManagerSrv;
using EasyWebApp.Data.Consts;
using EasyWebApp.Data.DbContext;
using EasyWebApp.Data.DbContextProvider;
using EasyWebApp.Data.Entities.QueryResultEntities;
using EasyWebApp.Data.Entities.SystemEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.CustomerDbProccessSrv
{
    public class CustomerDbProccessSrv : ICustomerDbProccessSrv
    {
        private readonly ICustomerDbConnStrManagerSrv _customerDbConnectionService;
        private readonly IDbContextProvider _provider;
        private CustomerDbContext _context;
        private readonly IMapper _mapper;

        public CustomerDbProccessSrv(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv,
                                     IDbContextProvider dbContextProvider,
                                     IMapper mapper)
        {
            this._customerDbConnectionService = customerDbConnStrManagerSrv;
            this._provider = dbContextProvider;
            this._mapper = mapper;
        } 

        public async Task<bool> WriteTableSchema(string dbGuid, string userGuid)
        {
            var dbConnInfo = await _customerDbConnectionService.GetCustomerDbInfoByGuid(dbGuid, userGuid);
            this._context = _provider.GetApplicationDbContext(dbConnInfo.BuildConnStr(), dbConnInfo.DbType);

            var dbConn = _context.Database.GetDbConnection();

            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }

            var tableSchema = GetTableSchema(dbConn, dbConnInfo.DbType);
            var columnSchema = GetColumnSchema(dbConn, tableSchema, dbConnInfo.DbType);
            var foreignKeySchema = GetForeignKey(dbConn, tableSchema, dbConnInfo.DbType);

            // Pouplate default value for SystemTableConfig
            List<SystemTableConfig> tableConfigs = new List<SystemTableConfig>();
            foreach (var info in tableSchema)
            {
                SystemTableConfig config = new SystemTableConfig()
                {
                    Name = info.TableName,
                    ExplicitName = info.TableName,
                    ActionGroup = "CRUD",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };
                tableConfigs.Add(config);
            }
            await _context.SystemTableConfigs.AddRangeAsync(tableConfigs);
            await _context.SaveChangesAsync();

            // Populate default value for SystemTableColumnConfig
            List<SystemTableColumnConfig> columnConfigs = new List<SystemTableColumnConfig>();
            switch (dbConnInfo.DbType)
            {
                case AppConst.DbSqlTypes.SQLServer:
                    var primaryKeySchema = GetMsSqlPrimaryKey(dbConn, tableSchema);
                    foreach (var info in columnSchema)
                    {
                        SystemTableColumnConfig config = new SystemTableColumnConfig()
                        {
                            TableId = tableConfigs.FirstOrDefault(t => t.Name == info.TableName).Id,
                            Name = info.ColumnName,
                            ExplicitName = info.ColumnName,
                            DataType = info.DataType,
                            ExplicitDataType = info.DataType,
                            OrdinalPosition = info.OrdinalPosition,
                            ColumnDefault = info.ColumnDefault,
                            CharacterMaximumLength = info.CharacterMaximumLength,
                            CharacterOctetLength = info.CharacterOctetLength,
                            IsNullable = info.IsNullable,
                            DisplayComponent = UIComponents.TextField,
                            IsPrimaryKey = primaryKeySchema.Any(t => t.ColumnName.Contains(info.ColumnName) && t.TableName == info.TableName),
                            IsForeignKey = foreignKeySchema.Any(t => t.SourceTableName == info.TableName && t.SourceColumnName == info.ColumnName && t.SourceColumnOrdinalPos == info.OrdinalPosition),
                            IsAutoIncrement = info.Extra == "auto_increment" ? true : false,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        };
                        columnConfigs.Add(config);
                    }
                    break;
                case AppConst.DbSqlTypes.MySQL:
                    foreach (var info in columnSchema)
                    {
                        SystemTableColumnConfig config = new SystemTableColumnConfig()
                        {
                            TableId = tableConfigs.FirstOrDefault(t => t.Name == info.TableName).Id,
                            Name = info.ColumnName,
                            ExplicitName = info.ColumnName,
                            DataType = info.DataType,
                            ExplicitDataType = info.DataType,
                            OrdinalPosition = info.OrdinalPosition,
                            ColumnDefault = info.ColumnDefault,
                            CharacterMaximumLength = info.CharacterMaximumLength,
                            CharacterOctetLength = info.CharacterOctetLength,
                            IsNullable = info.IsNullable,
                            DisplayComponent = UIComponents.TextField,
                            IsPrimaryKey = info.ColumnKey == "PRI",
                            IsForeignKey = foreignKeySchema.Any(t => t.SourceTableName == info.TableName && t.SourceColumnName == info.ColumnName && t.SourceColumnOrdinalPos == info.OrdinalPosition),
                            IsAutoIncrement = info.Extra == "auto_increment" ? true : false,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        };
                        columnConfigs.Add(config);
                    }
                    break;
                case AppConst.DbSqlTypes.PostgreSQL:
                    var primaryKeySchemaPostgres = GetPostgresPrimaryKey(dbConn, tableSchema);
                    foreach (var info in columnSchema)
                    {
                        SystemTableColumnConfig config = new SystemTableColumnConfig()
                        {
                            TableId = tableConfigs.FirstOrDefault(t => t.Name == info.TableName).Id,
                            Name = info.ColumnName,
                            ExplicitName = info.ColumnName,
                            DataType = info.DataType,
                            ExplicitDataType = info.DataType,
                            OrdinalPosition = info.OrdinalPosition,
                            ColumnDefault = info.ColumnDefault,
                            CharacterMaximumLength = info.CharacterMaximumLength,
                            CharacterOctetLength = info.CharacterOctetLength,
                            IsNullable = info.IsNullable,
                            DisplayComponent = UIComponents.TextField,
                            IsPrimaryKey = primaryKeySchemaPostgres.Any(t => t.ColumnName.Contains(info.ColumnName) && t.TableName == info.TableName),
                            IsForeignKey = foreignKeySchema.Any(t => t.SourceTableName == info.TableName && t.SourceColumnName == info.ColumnName && t.SourceColumnOrdinalPos == info.OrdinalPosition),
                            IsAutoIncrement = info.Extra == "auto_increment" ? true : false,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        };
                        columnConfigs.Add(config);
                    }
                    break;
            }
            await _context.SystemTableColumnConfigs.AddRangeAsync(columnConfigs);
            await _context.SaveChangesAsync();

            // Populate default value for SystemTableForeignKeyConifg
            List<SystemTableForeingKeyConfig> foreignKeyConfigs = new List<SystemTableForeingKeyConfig>();
            foreach (var foreignKey in foreignKeySchema)
            {
                SystemTableForeingKeyConfig config = new SystemTableForeingKeyConfig()
                {
                    FkName = foreignKey.FkName,
                    SourceTableName = foreignKey.SourceTableName,
                    SourceColumnName = foreignKey.SourceColumnName,
                    SourceColumnOrdinalPos = foreignKey.SourceColumnOrdinalPos,
                    RefrencedTableName = foreignKey.RefrencedTableName,
                    RefrencedColumnName = foreignKey.RefrencedColumnName,
                    RefrencedColumnOrdinalPos = foreignKey.RefrencedColumnOrdinalPos,
                    MappedRefrencedColumnName = foreignKey.RefrencedColumnName,
                    MappedRefrencedColumnOrdinalPos = foreignKey.RefrencedColumnOrdinalPos,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };
                foreignKeyConfigs.Add(config);
            }

            await _context.SystemTableForeingKeyConfigs.AddRangeAsync(foreignKeyConfigs);
            await _context.SaveChangesAsync();

            return true;
        }

        public List<TableSchemaQueryResult> GetTableSchema(DbConnection dbConn,
                                                           AppConst.DbSqlTypes dbType)
        {

            DataTable dbSchemaWithTables = dbConn.GetSchema("Tables");
            List <TableSchemaQueryResult> tableResults = _mapper.Map<List<TableSchemaQueryResult>>(dbSchemaWithTables.Rows);

            switch (dbType)
            {
                case AppConst.DbSqlTypes.MySQL:
                    tableResults = tableResults.Where(t =>
                                            t.TableType == "BASE TABLE" &&
                                            !AppConst.SysTableNames.Contains(t.TableName) &&
                                            !AppConst.MySqlSystemSchemaName.Contains(t.TableSchema)).OrderBy(t => t.TableName).ToList();
                    break;
                default:
                    tableResults = tableResults.Where(t => t.TableType == "BASE TABLE" && !AppConst.SysTableNames.
                                                                                                    Contains(t.TableName)).
                                                                                                    OrderBy(t => t.TableName).
                                                                                                    ToList();
                    break;


            }

            return tableResults;
        }

        public List<TableColumnSchemaQueryResult> GetColumnSchema(DbConnection dbConn, 
                                                                  List<TableSchemaQueryResult> tables,
                                                                  AppConst.DbSqlTypes dbType)
        {
            List<TableColumnSchemaQueryResult> columnResults = new List<TableColumnSchemaQueryResult>();

            DataTable dbSchemaWithTableColumns = dbConn.GetSchema("Columns");

            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }

            switch (dbType)
            {
                case Data.Consts.AppConst.DbSqlTypes.MySQL:
                    columnResults = _mapper.Map<List<TableColumnSchemaQueryResult>>(dbSchemaWithTableColumns.Rows)
                                           .Where(t => tables.Any(s => s.TableName == t.TableName))
                                           .ToList();
                    break;
                default:
                    string commandText = "";
                    string scriptFileName = "";
                    DataTable IdentityColumns = new DataTable();
                    Assembly thisAssembly = Assembly.GetExecutingAssembly();

                    if(dbType == AppConst.DbSqlTypes.SQLServer)
                    {
                        scriptFileName = "EasyWebApp.API.SqlScript.MsSqlGetIdentity.sql";
                    }
                    else
                    {
                        scriptFileName = "EasyWebApp.API.SqlScript.PostgresGetIdentity.sql";
                    }

                    using (Stream s = thisAssembly.GetManifestResourceStream(scriptFileName))
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            commandText = sr.ReadToEnd();
                        }
                    }

                    try
                    {
                        DbCommand command = dbConn.CreateCommand();
                        command.CommandText = commandText;
                        command.CommandType = CommandType.Text;

                        if (dbConn.State == ConnectionState.Closed)
                        {
                            dbConn.Open();
                        }

                        DbDataReader reader = command.ExecuteReader();
                        IdentityColumns.Load(reader);
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception.Message: {0}", ex.Message);
                    }

                    dbSchemaWithTableColumns.Columns.Add("COLUMN_KEY", typeof(String));
                    dbSchemaWithTableColumns.Columns.Add("EXTRA", typeof(String));

                    columnResults = _mapper.Map<List<TableColumnSchemaQueryResult>>(dbSchemaWithTableColumns.Rows)
                       .Where(t => tables.Any(s => s.TableName == t.TableName))
                       .ToList();

                    if (IdentityColumns.Rows.Count != 0)
                    {
                        foreach (DataRow row in IdentityColumns.Rows)
                        {
                            var selectedColumn = columnResults.FirstOrDefault(t => t.TableName == row.Field<string>("TABLENAME") &&
                                                                                    t.ColumnName == row.Field<string>("COLUMNNAME"));
                            if(selectedColumn != null)
                            {
                                selectedColumn.Extra = "auto_increment";
                            }
                        }
                    }
                    break;

            }

            return columnResults;
        }

        public List<ForeignKeyQueryResult> GetForeignKey(DbConnection dbConn,
                                                         List<TableSchemaQueryResult> tables,
                                                         AppConst.DbSqlTypes dbType)
        {
            List<ForeignKeyQueryResult> results = new List<ForeignKeyQueryResult>();
            DataTable dbSchemaWithForeignKeys = new DataTable();

            string commandText = "";
            string scriptFileName = "";
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            switch (dbType)
            {
                case AppConst.DbSqlTypes.SQLServer:
                    scriptFileName = "EasyWebApp.API.SqlScript.MsSqlGetForeignKey.sql";
                    break;
                case AppConst.DbSqlTypes.MySQL:
                    scriptFileName = "EasyWebApp.API.SqlScript.MySqlGetForeignKey.sql";
                    break;
                case AppConst.DbSqlTypes.PostgreSQL:
                    scriptFileName = "EasyWebApp.API.SqlScript.PostgresGetForeignKey.sql";
                    break;
            }

            using (Stream s = thisAssembly.GetManifestResourceStream(scriptFileName))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    commandText = sr.ReadToEnd();
                    if (dbType == AppConst.DbSqlTypes.MySQL)
                    {
                        commandText = string.Format(commandText, tables[0].TableSchema);
                    }
                }
            }


            try
            {
                DbCommand command = dbConn.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;

                DbDataReader reader = command.ExecuteReader();
                dbSchemaWithForeignKeys.Load(reader);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }

            results = _mapper.Map<List<ForeignKeyQueryResult>>(dbSchemaWithForeignKeys.Rows);

            return results.Where(t => tables.Any(s => s.TableName == t.SourceTableName)).ToList();
        }

        public List<PrimaryKeyQueryResult> GetMsSqlPrimaryKey(DbConnection dbConn,
                                                              List<TableSchemaQueryResult> tables)
        {
            List<PrimaryKeyQueryResult> results = new List<PrimaryKeyQueryResult>();
            DataTable dbSchemaWithPrimaryKeys = new DataTable();
            string commandText = "";
            string scriptFileName = "EasyWebApp.API.SqlScript.MsSqlGetPrimaryKey.sql";
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }

            using (Stream s = thisAssembly.GetManifestResourceStream(scriptFileName))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    commandText = sr.ReadToEnd();
                }
            }


            try
            {
                DbCommand command = dbConn.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;

                DbDataReader reader = command.ExecuteReader();
                dbSchemaWithPrimaryKeys.Load(reader);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }

            results = _mapper.Map<List<PrimaryKeyQueryResult>>(dbSchemaWithPrimaryKeys.Rows);

            return results.Where(t => tables.Any(s => s.TableName == t.TableName) && t.SchemaName == tables[0].TableSchema).ToList();
        }

        public List<PrimaryKeyQueryResult> GetPostgresPrimaryKey(DbConnection dbConn,
                                                                 List<TableSchemaQueryResult> tables)
        {
            List<PrimaryKeyQueryResult> results = new List<PrimaryKeyQueryResult>();
            DataTable dbSchemaWithPrimaryKeys = new DataTable();
            string commandText = "";
            string scriptFileName = "EasyWebApp.API.SqlScript.PostgresGetPrimaryKey.sql";
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }

            using (Stream s = thisAssembly.GetManifestResourceStream(scriptFileName))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    commandText = sr.ReadToEnd();
                }
            }


            try
            {
                DbCommand command = dbConn.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;

                DbDataReader reader = command.ExecuteReader();
                dbSchemaWithPrimaryKeys.Load(reader);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }

            results = _mapper.Map<List<PrimaryKeyQueryResult>>(dbSchemaWithPrimaryKeys.Rows);

            return results.Where(t => tables.Any(s => s.TableName == t.TableName) && t.SchemaName == tables[0].TableSchema).ToList();
        }

        public async Task CreateSystemTable(string dbGuid, string userGuid)
        {
            var dbConninfo = await _customerDbConnectionService.GetCustomerDbInfoByGuid(dbGuid, userGuid);
            this._context = _provider.GetApplicationDbContext(dbConninfo.BuildConnStr(), dbConninfo.DbType);

            var dbConn = _context.Database.GetDbConnection();

            string commandText = "";
            string scriptFileName = "";
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            switch (dbConninfo.DbType)
            {
                case Data.Consts.AppConst.DbSqlTypes.MySQL:
                    scriptFileName = "EasyWebApp.API.SqlScript.MySqlSysTableCreate.sql";
                    break;
                case Data.Consts.AppConst.DbSqlTypes.SQLServer:
                    scriptFileName = "EasyWebApp.API.SqlScript.MsSqlSysTableCreate.sql";
                    break;
                case AppConst.DbSqlTypes.PostgreSQL:
                    scriptFileName = "EasyWebApp.API.SqlScript.PostgresSystTableCreate.sql";
                    break;
            }

            using (Stream s = thisAssembly.GetManifestResourceStream(scriptFileName))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    commandText = sr.ReadToEnd();
                }
            }

            try
            {
                DbCommand command = dbConn.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;

                dbConn.Open();

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}. {1}", reader[0], reader[1]);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }

        }
    }
}

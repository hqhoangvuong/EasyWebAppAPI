namespace EasyWebApp.Data.Consts
{
    public enum UIComponents
    {
        TextField,
        Checkbox,
        DateTime,
        Radio,
        Switch,
        Select,
        List
    };

    public static class AppConst
    {
        public static readonly string[] SysTableNames = {
            "__EFMigrationsHistory",
            "AspNetRoles",
            "AspNetUsers",
            "SystemMasterConfigs",
            "SystemTableConfigs",
            "SystemTableForeingKeyConfigs",
            "AspNetRoleClaims",
            "AspNetUserClaims",
            "AspNetUserLogins",
            "AspNetUserRoles",
            "AspNetUserTokens",
            "SystemTableColumnConfigs",
            "sysdiagrams"
        };

        public static readonly string[] SqlStringDataType =
        {
            "CHAR",
            "VARCHAR",
            "NVARCHAR",
            "BINARY",
            "VARBINARY",
            "TINYBLOB",
            "TINYTEXT",
            "TEXT",
            "BLOB",
            "MEDIUMTEXT",
            "MEDIUMBLOB",
            "LONGTEXT",
            "LONGBLOB"
        };

        public static readonly string[] SqlNumericDataType =
        {
            "BIT",
            "TINYINT",
            "BOOL",
            "BOOLEAN",
            "SMALLINT",
            "MEDIUMINT",
            "INT",
            "INTEGER",
            "INTEGER",
            "MONEY"
        };

        public static readonly string[] MySqlSystemSchemaName =
        {
            "performance_schema",
            "information_schema",
            "mysql",
            "sys"
        };

        public static readonly string[] SqlFloatingPointDataType =
        {
            "FLOAT",
            "DOUBLE",
        };

        public enum DbSqlTypes
        {
            SQLServer,
            MySQL,
            PostgreSQL
        };
    }
}

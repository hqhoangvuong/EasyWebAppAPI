using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.Data.Entities.QueryResultEntities
{
    public class TableColumnSchemaQueryResult
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int OrdinalPosition { get; set; }
        public string ColumnKey { get; set; }
        public string ColumnDefault { get; set; }
        public string IsNullable { get; set; }
        public string DataType { get; set; }
        public int CharacterMaximumLength { get; set; }
        public int CharacterOctetLength { get; set; }
    }
}

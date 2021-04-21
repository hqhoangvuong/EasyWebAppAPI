using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.Data.Entities.QueryResultEntities
{
    public class PrimaryKeyQueryResult
    {
        public string SchemaName { get; set; }
        public string PKName { get; set; }
        public string ColumnName { get; set; }
        public string TableName { get; set; }
    }
}

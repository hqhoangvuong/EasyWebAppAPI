using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.Data.Entities.QueryResultEntities
{
    public class ForeignKeyQueryResult
    {
        public string FkName { get; set; }
        public string SourceTableName { get; set; }
        public string SourceColumnName { get; set; }
        public int SourceColumnOrdinalPos { get; set; }
        public string RefrencedTableName { get; set; }
        public string RefrencedColumnName { get; set; }
        public int RefrencedColumnOrdinalPos { get; set; }
    }
}

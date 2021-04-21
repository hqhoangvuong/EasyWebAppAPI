using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.Data.Entities.QueryResultEntities
{
    public class QueryResult
    {
        public int ErrorNumber { get; set; } 
        public int ErrorState { get; set; }
        public int ErrorSeverity { get; set; } 
        public int ErrorProcedure { get; set; }  
        public int ErrorLine { get; set; } 
        public string ErrorMessage { get; set; }
    }
}

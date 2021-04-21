using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericWeb.Data.ViewModels
{
    public class TableConfViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExplicitName { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }
        public List<TableColumnConfViewModel> Columns { get; set; }
    }
}

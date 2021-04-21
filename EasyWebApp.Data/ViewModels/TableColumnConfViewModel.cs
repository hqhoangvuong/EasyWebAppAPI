using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericWeb.Data.ViewModels
{
    public class TableColumnConfViewModel
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string Name { get; set; }
        public string ExplicitName { get; set; }
        public string DataType { get; set; }
        public string ExplicitDataType { get; set; }
        public int OrdinalPosition { get; set; }
        public string ColumnDefault { get; set; }
        public int CharacterMaximumLength { get; set; }
        public int CharacterOctetLength { get; set; }
        public string IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }
    }
}

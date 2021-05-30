using EasyWebApp.Data.Consts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyWebApp.Data.Entities.SystemEntities
{
    public class SystemTableColumnConfig : BaseEntity, IBaseEntity
    {

        [ForeignKey("Table")]
        [Required]
        public int TableId { get; set; }

        public virtual SystemTableConfig Table { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ExplicitName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string PropertyName { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string DataType { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ExplicitDataType { get; set; }

        public int OrdinalPosition { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ColumnDefault { get; set; }

        public int CharacterMaximumLength { get; set; }

        public int CharacterOctetLength { get; set; }

        public UIComponents DisplayComponent { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string IsNullable { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsPrimaryKey { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsForeignKey { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsHidden { get; set; }

        [DefaultValue(false)]
        public bool IsAutoIncrement { get; set; }
    }
}

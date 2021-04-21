using System.ComponentModel.DataAnnotations.Schema;

namespace EasyWebApp.Data.Entities.SystemEntities
{
    public class SystemTableForeingKeyConfig : BaseEntity, IBaseEntity
    {

        [Column(TypeName = "varchar(200)")]
        public string FkName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string SourceTableName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string SourceColumnName { get; set; }

        public int SourceColumnOrdinalPos { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string RefrencedTableName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string RefrencedColumnName { get; set; }

        public int RefrencedColumnOrdinalPos { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string MappedRefrencedColumnName { get; set; }

        public int MappedRefrencedColumnOrdinalPos { get; set; }
    }
}

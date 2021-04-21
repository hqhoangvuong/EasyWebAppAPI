using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyWebApp.Data.Entities.SystemEntities
{
    public class SystemTableConfig : BaseEntity, IBaseEntity
    {

        [Column(TypeName = "varchar(200)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required]
        public string ExplicitName { get; set; }

        [DefaultValue(false)]
        public bool IsHidden { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required]
        [DefaultValue("CRUD")]
        public string ActionGroup { get; set; }

        public virtual List<SystemTableColumnConfig> Columns { get; set; }
    }
}

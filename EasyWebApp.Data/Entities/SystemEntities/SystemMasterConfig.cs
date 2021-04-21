using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyWebApp.Data.Entities.SystemEntities
{
    public class SystemMasterConfig : BaseEntity, IBaseEntity
    {
        [Column(TypeName = "varchar(200)")]
        [Required]
        public string ConfigName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ConfigValue { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

    }
}

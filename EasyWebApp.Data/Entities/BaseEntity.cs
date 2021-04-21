using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyWebApp.Data.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}

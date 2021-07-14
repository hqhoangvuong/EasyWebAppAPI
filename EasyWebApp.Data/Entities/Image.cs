using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyWebApp.Data.Entities
{
    public class Image
    {
        public int id { get; set; }
        public string Img { get; set; }
    }
}

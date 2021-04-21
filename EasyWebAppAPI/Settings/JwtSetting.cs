using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Settings
{
    public class JwtSetting
    {
        public string SecretKey { get; set; }
        public double Expiration { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.Data.Entities.AuthenticationEnties
{
    public class JwtToken
    {
        public string Id { get; set; }
        public string TokenType { get; set; } = "JWT Bearer";
        public string AccessToken { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}

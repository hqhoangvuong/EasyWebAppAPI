using EasyWebApp.Data.Entities.AuthenticationEnties;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.AuthSrv
{
    public interface IAuthSrv
    {
        Task<JwtSecurityToken> GetJwtTokenAsync(ApplicationUser user, params Claim[] extraClaims);
    }
}

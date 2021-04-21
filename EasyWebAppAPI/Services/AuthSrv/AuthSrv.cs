using EasyWebApp.API.Settings;
using EasyWebApp.Data.Entities.AuthenticationEnties;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using IdentityModel;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.AuthSrv
{
    public class AuthSrv : IAuthSrv
    {
        private readonly JwtSetting _jwtSetting;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthSrv(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSetting> jwtSetting)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSetting = jwtSetting.Value;
        }

        private static IEnumerable<Claim> GetTokenClaims(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString()),
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.NickName, user.UserName),
                new Claim(JwtClaimTypes.Email, user.Email)
            };
        }

        private async Task<IEnumerable<Claim>> GetRoleClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var rolename in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rolename));
                claims.Add(new Claim(JwtClaimTypes.Role, rolename));

                var role = await _roleManager.FindByNameAsync(rolename);
                if (role == null) continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            return claims;
        }

        public async Task<JwtSecurityToken> GetJwtTokenAsync(ApplicationUser user, params Claim[] extraClaims)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roleClaims = await GetRoleClaims(user);
            var claims = GetTokenClaims(user).Union(userClaims).Union(roleClaims).Union(extraClaims);

            return new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSetting.Expiration),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey)),
                    SecurityAlgorithms.HmacSha256)
                );
        }
    }
}

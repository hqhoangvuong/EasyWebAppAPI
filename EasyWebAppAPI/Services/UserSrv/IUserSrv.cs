using EasyWebApp.Data.Entities.AuthenticationEnties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.UserSrv
{
    public interface IUserSrv
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser entity, string password = null);
        Task<ApplicationUser> ValidateUserAsync(string email, string password);
        Task UpdatePasswordAsync(ApplicationUser entity, string password);
    }
}

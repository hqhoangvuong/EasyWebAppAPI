using EasyWebApp.Data.Entities.AuthenticationEnties;
using EasyWebApp.Data.Exception;
using EasyWebApp.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.UserSrv
{
    public class UserSrv : IUserSrv
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSrv(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<ApplicationUser> CreateAsync(ApplicationUser entity, string password = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserInfo(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            var loggedUser = await _userManager.FindByIdAsync(userId);
            if (loggedUser == null)
                throw new ModelStateException(nameof(userId), "Invalid user Id");
            return loggedUser;
        }

        public Task UpdatePasswordAsync(ApplicationUser entity, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> ValidateUserAsync(string email, string password)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            var entity = await _userManager.FindByEmailAsync(email);
            if (entity == null || !await _userManager.CheckPasswordAsync(entity, password))
                throw new ModelStateException(nameof(password), "Invalid username or password");

            return entity;
        }
    }
}

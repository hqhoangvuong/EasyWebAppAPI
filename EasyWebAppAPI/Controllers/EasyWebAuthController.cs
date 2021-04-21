using EasyWebApp.API.Services.AuthSrv;
using EasyWebApp.API.Services.UserSrv;
using EasyWebApp.Data.Entities.AuthenticationEnties;
using EasyWebApp.Data.Exception;
using EasyWebApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Controllers
{
    [Route("api/[controller]")] 
    [AllowAnonymous]
    [ApiController]
    public class EasyWebAuthController : ControllerBase
    {
        private readonly IUserSrv _userService;
        private readonly IAuthSrv _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EasyWebAuthController> _logger;

        public EasyWebAuthController(IUserSrv userService, 
                                     IAuthSrv authService, 
                                     UserManager<ApplicationUser> userManager, 
                                     ILogger<EasyWebAuthController> logger)
        {
            _userService = userService;
            _authService = authService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(JwtToken), 200)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userService.ValidateUserAsync(request.Email, request.Password);
                var securityToken = await _authService.GetJwtTokenAsync(user);

                return Ok(new JwtToken
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    ExpiredTime = securityToken.ValidTo
                });
            }
            catch (Exception ex) when (!(ex is ModelStateException))
            {
                _logger.LogWarning("Failed to login");
                return StatusCode(500);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel newUser)
        {
            var currentUser = _userManager.Users.OrderByDescending(p => p.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = newUser.Username,
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName
                };

                var result = await _userManager.CreateAsync(user, newUser.Password);
                return Ok(result);
            }

            return BadRequest();
        }
    }
}

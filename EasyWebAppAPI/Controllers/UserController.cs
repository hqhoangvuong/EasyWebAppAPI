using AutoMapper;
using EasyWebApp.API.Extensions;
using EasyWebApp.API.Services.UserSrv;
using EasyWebApp.Data.Entities.AuthenticationEnties;
using EasyWebApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSrv _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserSrv userSrv,
                              UserManager<ApplicationUser> userManager,
                              ILogger<UserController> logger,
                              IMapper mapper)
        {
            this._userService = userSrv;
            this._userManager = userManager;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserInfoViewModel>> GetUserInfo()
        {
            var userId = User.GetId();
            var loggedUser = await _userService.GetUserInfo(userId);
            var result = _mapper.Map<UserInfoViewModel>(loggedUser);
            return Ok(result);
        }
    }
}

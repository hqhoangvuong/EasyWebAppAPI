using EasyWebApp.API.Extensions;
using EasyWebApp.API.Services.CustomerDbConnStrManagerSrv;
using EasyWebApp.Data.Entities.ServiceWebEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DbConnectionController : ControllerBase
    {
        private readonly ICustomerDbConnStrManagerSrv _customerDbConnectionService;

        public DbConnectionController(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv)
        {
            this._customerDbConnectionService = customerDbConnStrManagerSrv;
        }

        [HttpPost("RegisterNewDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDbInfo>> RegisterDb(UserDbInfo newDb)
        {
            return Ok(await _customerDbConnectionService.RegisterDb(newDb, User.GetId()));
        }

        [HttpPut("RegisterBussinessName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDbInfo>> RegisterBussinessName(string dbGuid, string bussinessName)
        {
            return Ok(await _customerDbConnectionService.PopulateBussinessName(dbGuid, User.GetId(), bussinessName));
        }

    }
}

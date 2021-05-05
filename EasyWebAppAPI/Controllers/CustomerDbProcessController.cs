using AutoMapper;
using EasyWebApp.API.Extensions;
using EasyWebApp.API.Services.CustomerDbConnStrManagerSrv;
using EasyWebApp.API.Services.CustomerDbProccessSrv;
using EasyWebApp.Data.DbContext;
using EasyWebApp.Data.DbContextProvider;
using EasyWebApp.Data.Entities.QueryResultEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerDbProcessController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerDbProccessSrv _customerDbProccessSrv;

        public CustomerDbProcessController(IMapper mapper,
                                           ICustomerDbProccessSrv customerDbProccessSrv)
        {
            this._mapper = mapper;
            this._customerDbProccessSrv = customerDbProccessSrv;
        }

        [HttpGet("PopulateDefaultSchemaConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> WirteSchemaConfig(string dbGuid)
        {
            var results = await _customerDbProccessSrv.WriteTableSchema(dbGuid, User.GetId());
            return Ok(results);
        }

        [HttpPost("CreateSystemTable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateSystemTable(string dbGuid)
        {
            await _customerDbProccessSrv.CreateSystemTable(dbGuid, User.GetId());
            return Ok();
        }
    }
}

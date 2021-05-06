using EasyWebApp.API.Commons;
using EasyWebApp.API.Extensions;
using EasyWebApp.API.Services.CustomerDbConnStrManagerSrv;
using EasyWebApp.Data.DbContext;
using EasyWebApp.Data.DbContextProvider;
using EasyWebApp.Data.Entities.SystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterConfigController : ControllerBase
    {
        private readonly ICustomerDbConnStrManagerSrv _customerDbConnStrManagerSrv;
        private readonly IDbContextProvider _provider;

        public MasterConfigController(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv,
                                      IDbContextProvider dbContextProvider)
        {
            this._customerDbConnStrManagerSrv = customerDbConnStrManagerSrv;
            this._provider = dbContextProvider;
        }

        private async Task<CustomerDbContext> GetDbContext(string dbGuid)
        {
            var userGuid = User.GetId();
            var dbConnInfo = await _customerDbConnStrManagerSrv.GetCustomerDbInfoByGuid(dbGuid, userGuid);

            return _provider.GetApplicationDbContext(dbConnInfo.BuildConnStr().ToString(), dbConnInfo.DbType);
        }

        [HttpGet("GetAllMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SystemMasterConfig>>> GetAllMasterConfig(string dbGuid)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemMasterConfigs.ToListAsync();
            return Ok(results);
        }

        [HttpGet("GetMasterConfigById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemMasterConfig>> GetMasterConfigById(string dbGuid, int configId)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemMasterConfigs.FirstOrDefaultAsync(t => t.Id == configId);

            if (results == null)
            {
                return CheckData<SystemMasterConfig>.ItemNotFound(configId);
            }

            return Ok(results);
        }

        [HttpPost("CreateMasterConfig")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemMasterConfig>> CreateMasterConfig(string dbGuid, [FromBody] SystemMasterConfig newMasterConfig)
        {
            var context = await GetDbContext(dbGuid);
            var exsited = await context.SystemMasterConfigs.FirstOrDefaultAsync(t => t.ConfigName == newMasterConfig.ConfigName);

            if(exsited != null)
            {
                return CheckData<SystemMasterConfig>.ItemStringExists("ConfigName", newMasterConfig.ConfigName);
            }

            newMasterConfig.CreatedDate = DateTime.UtcNow;
            newMasterConfig.ModifiedDate = DateTime.UtcNow;
            newMasterConfig.IsActive = true;

            await context.SystemMasterConfigs.AddAsync(newMasterConfig);
            await context.SaveChangesAsync();

            return Created("", newMasterConfig);
        }

        [HttpPut("UpdateMasterConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemMasterConfig>> UpdateTableConfig(string dbGuid, SystemMasterConfig updatedConfig)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemMasterConfigs.FirstOrDefaultAsync(t => t.Id == updatedConfig.Id);

            if (results == null)
            {
                return CheckData<SystemMasterConfig>.ItemNotFound(updatedConfig.Id);
            }

            results.ConfigName = updatedConfig.ConfigName;
            results.ConfigValue = updatedConfig.ConfigValue;
            results.IsActive = updatedConfig.IsActive;
            results.ModifiedDate = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Ok(results);
        }
    }
}

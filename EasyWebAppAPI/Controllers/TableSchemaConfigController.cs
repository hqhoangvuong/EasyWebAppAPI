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
    public class TableSchemaConfigController : ControllerBase
    {
        private readonly ICustomerDbConnStrManagerSrv _customerDbConnStrManagerSrv;
        private readonly IDbContextProvider _provider;

        public TableSchemaConfigController(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv,
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

        [HttpGet("GetAllTableConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SystemTableConfig>>> GetAllTableConfig(string dbGuid)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemTableConfigs.ToListAsync();
            return Ok(results);
        }

        [HttpGet("GetTableConfigById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableConfig>> GetTableConfigById(string dbGuid, int configId)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemTableConfigs.FirstOrDefaultAsync(t => t.Id == configId);

            if(results == null)
            {
                return CheckData<SystemTableConfig>.ItemNotFound(configId);
            }

            return Ok(results);
        }

        [HttpPut("UpdateTableConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableConfig>> UpdateTableConfig(string dbGuid, SystemTableConfig updatedConfig)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemTableConfigs.FirstOrDefaultAsync(t => t.Id == updatedConfig.Id);

            if (results == null)
            {
                return CheckData<SystemTableConfig>.ItemNotFound(updatedConfig.Id);
            }

            results.ExplicitName = updatedConfig.ExplicitName;
            results.IsHidden = updatedConfig.IsHidden;
            results.ActionGroup = updatedConfig.ActionGroup;
            results.ModifiedDate = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return results;
        }
    }
}

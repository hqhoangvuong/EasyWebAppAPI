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
    public class TableColumnSchemaController : ControllerBase
    {
        private readonly ICustomerDbConnStrManagerSrv _customerDbConnStrManagerSrv;
        private readonly IDbContextProvider _provider;

        public TableColumnSchemaController(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv,
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

        [HttpGet("GetAllTableColumnConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SystemTableColumnConfig>>> GetAllTableConfig(string dbGuid)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemTableColumnConfigs.ToListAsync();
            return Ok(results);
        }

        [HttpGet("GetTableColumnConfigById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableColumnConfig>> GetTableColumnConfigById(string dbGuid, int configId)
        {
            var context = await GetDbContext(dbGuid);
            var result = await context.SystemTableColumnConfigs.FirstOrDefaultAsync(t => t.Id == configId);

            if (result == null)
            {
                return CheckData<SystemTableColumnConfig>.ItemNotFound(configId);
            }

            return Ok(result);
        }

        [HttpGet("GetColumnConfigByTableId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SystemTableColumnConfig>>> GetTableColumnConfigByTableId(string dbGuid, int tableId)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemTableColumnConfigs.Where(t => t.TableId == tableId).ToListAsync();

            if (results == null)
            {
                return CheckData<SystemTableColumnConfig>.ItemNotFound(tableId);
            }

            return Ok(results);
        }

        [HttpPut("UpdateTableColumnConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableColumnConfig>> UpdateTableColumnConfig(string dbGuid, SystemTableColumnConfig updatedConfig)
        {
            var context = await GetDbContext(dbGuid);
            var results = await context.SystemTableColumnConfigs.FirstOrDefaultAsync(t => t.Id == updatedConfig.Id);

            if (results == null)
            {
                return CheckData<SystemTableColumnConfig>.ItemNotFound(updatedConfig.Id);
            }

            results.ExplicitName = updatedConfig.ExplicitName;
            results.ExplicitDataType = updatedConfig.ExplicitDataType;
            results.DisplayComponent = updatedConfig.DisplayComponent;
            results.IsHidden = updatedConfig.IsHidden;
            results.ModifiedDate = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return results;
        }
    }
}

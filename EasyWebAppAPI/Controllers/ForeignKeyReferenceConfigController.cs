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
    public class ForeignKeyReferenceConfigController : ControllerBase
    {
        private readonly ICustomerDbConnStrManagerSrv _customerDbConnStrManagerSrv;
        private readonly IDbContextProvider _provider;

        public ForeignKeyReferenceConfigController(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv,
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

        [HttpGet("GetForeignKeyConfigById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableForeingKeyConfig>> GetForeignKeyConfigById(string dbGuid, int configId)
        {
            var context = await GetDbContext(dbGuid);
            var result = await context.SystemTableForeingKeyConfigs.FirstOrDefaultAsync(t => t.Id == configId);

            if (result == null)
            {
                return CheckData<SystemTableConfig>.ItemNotFound(configId);
            }

            return Ok(result);
        }

        [HttpGet("GetAllForeignKeyConfigBySourceTableId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SystemTableForeingKeyConfig>>> GetAllForeignKeyConfigByTableId(string dbGuid, int tableId)
        {
            var context = await GetDbContext(dbGuid);

            var table = await context.SystemTableConfigs.FirstOrDefaultAsync(t => t.Id == tableId);
            if (table == null)
            {
                return CheckData<SystemTableConfig>.ItemNotFound(tableId);
            }


            var result = await context.SystemTableForeingKeyConfigs.Where(t => t.SourceTableName == table.Name).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetAllForeignKeyConfigBySourceColumnId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableForeingKeyConfig>> GetAllForeignKeyConfigByColumnId(string dbGuid, int columnId)
        {
            var context = await GetDbContext(dbGuid);

            var column = await context.SystemTableColumnConfigs.FirstOrDefaultAsync(t => t.Id == columnId);
            if (column == null)
            {
                return CheckData<SystemTableColumnConfig>.ItemNotFound(columnId);
            }


            var result = await context.SystemTableForeingKeyConfigs.FirstOrDefaultAsync(t => t.SourceColumnName == column.Name);

            return Ok(result);
        }

        [HttpPut("UpdateForeignKeyConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SystemTableForeingKeyConfig>> UpdateForeignKeyConfig(string dbGuid, 
                                                                                            [FromBody] SystemTableForeingKeyConfig updatedConfig)
        {
            var context = await GetDbContext(dbGuid);

            var result = await context.SystemTableForeingKeyConfigs.FirstOrDefaultAsync(t => t.Id == updatedConfig.Id);
            if (result == null)
            {
                return CheckData<SystemTableForeingKeyConfig>.ItemNotFound(updatedConfig.Id);
            }

            result.MappedRefrencedColumnName = updatedConfig.MappedRefrencedColumnName;
            result.MappedRefrencedColumnOrdinalPos = updatedConfig.MappedRefrencedColumnOrdinalPos;
            result.ModifiedDate = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return result;
        }

    }
}

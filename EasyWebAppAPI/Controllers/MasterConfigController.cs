using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
using EasyWebApp.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.IO;

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
        public async Task<ActionResult<SystemMasterConfig>> CreateMasterConfig(string dbGuid, [FromBody] IEnumerable<SystemMasterConfig> newMasterConfigs)
        {
            var context = await GetDbContext(dbGuid);
            foreach (var item in newMasterConfigs)
            {
                item.CreatedDate = DateTime.UtcNow;
                item.ModifiedDate = DateTime.UtcNow;
                item.IsActive = true;
                await context.SystemMasterConfigs.AddAsync(item);
            }
            
            await context.SaveChangesAsync();

            return Created("", newMasterConfigs);
        }

        [HttpPost("UploadImage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task UploadImage(string dbGuid, [FromBody] Data.Entities.Image image)
        {
            Account account = new Account("dsnt8hyn6",
                                          "121517243286974",
                                          "q-rZlX2PIYFPuLOmPlrZso1UYx4");

            var context = await GetDbContext(dbGuid);
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.Img)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            var imageUrl = Convert.ToString(uploadResult.Url);

            var result = await context.SystemMasterConfigs.FirstOrDefaultAsync(t => t.ConfigName == "BussinessLogo");
            if(result != null)
            {
                result.ConfigValue = imageUrl;
                result.ModifiedDate = DateTime.UtcNow;
            }
            else
            {
                var newConfig = new SystemMasterConfig()
                {
                    ConfigName = "BussinessLogo",
                    ConfigValue = imageUrl,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsActive = true
                };

                await context.SystemMasterConfigs.AddAsync(newConfig);
            }

            await context.SaveChangesAsync();
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

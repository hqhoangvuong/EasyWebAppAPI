using AutoMapper;
using EasyWebApp.API.Extensions;
using EasyWebApp.API.Services.CustomerDbConnStrManagerSrv;
using EasyWebApp.Data.Entities.ServiceWebEntities;
using EasyWebApp.Data.ViewModels;
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
    public class CustomerDbStatisticsController : ControllerBase
    {
        private readonly ICustomerDbConnStrManagerSrv service;
        private readonly IMapper _mapper;

        public CustomerDbStatisticsController(ICustomerDbConnStrManagerSrv customerDbConnStrManagerSrv,
                                              IMapper mapper)
        {
            this.service = customerDbConnStrManagerSrv;
            this._mapper = mapper;
        }

        [HttpGet("GetStatistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DbInfoViewModel>>> GetStatistics()
        {
            var raws = await service.GetAllDbOfUser(User.GetId());
            var result = _mapper.Map<List<DbInfoViewModel>>(raws);
            return Ok(result);
        }
    }
}

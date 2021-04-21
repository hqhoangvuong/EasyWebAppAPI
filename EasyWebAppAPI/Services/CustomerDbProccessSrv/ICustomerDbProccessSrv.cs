using EasyWebApp.Data.Entities.QueryResultEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.CustomerDbProccessSrv
{
    public interface ICustomerDbProccessSrv
    {
        Task<bool> WriteTableSchema(string dbGuid, string userGuid);
        Task CreateSystemTable(string dbGuid, string userGuid);
    }
}

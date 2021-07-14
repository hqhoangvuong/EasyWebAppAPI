using EasyWebApp.Data.Entities.ServiceWebEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.CustomerDbConnStrManagerSrv
{
    public interface ICustomerDbConnStrManagerSrv
    {
        Task<UserDbInfo> GetCustomerDbInfoByGuid(string dbInfoGuid, string userId);
        Task<UserDbInfo> RegisterDb(UserDbInfo newDbInfo, string userGuid);
        Task<UserDbInfo> PopulateBussinessName(string dbInfoGuid, string userId, string bussinessName);
        Task<IEnumerable<UserDbInfo>> GetAllDbOfUser(string userId);
    }
}

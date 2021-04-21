using EasyWebApp.Data.DbContext;
using EasyWebApp.Data.Entities.ServiceWebEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWebApp.API.Services.CustomerDbConnStrManagerSrv
{
    public class CustomerDbConnStrManagerSrv : ICustomerDbConnStrManagerSrv
    {
        private readonly EasyWebDbContext _context;

        public CustomerDbConnStrManagerSrv(EasyWebDbContext easyWebDbContext)
        {
            this._context = easyWebDbContext;
        }

        public async Task<UserDbInfo> GetCustomerDbInfoByGuid(string dbInfoGuid, string userId)
        {
            return await _context.UserDatabaseInfos.FirstOrDefaultAsync(t => t.UserId == userId && t.Guid == dbInfoGuid);
        }

        public async Task<UserDbInfo> RegisterDb(UserDbInfo newDbInfo, string userGuid)
        {
            newDbInfo.UserId = userGuid;
            newDbInfo.Guid = Guid.NewGuid().ToString();
            newDbInfo.CreatedDate = DateTime.UtcNow;
            newDbInfo.ModifiedDate = DateTime.UtcNow;

            _context.UserDatabaseInfos.Add(newDbInfo);
            await _context.SaveChangesAsync();
            return newDbInfo;
        }
    }
}

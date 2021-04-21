using Microsoft.AspNetCore.Mvc;

namespace EasyWebApp.API.Commons
{
    public static class CheckData<T>
    {
        public static NotFoundObjectResult ItemNotFound(int Id)
        {
            return new NotFoundObjectResult($"ID of {typeof(T).Name} not found : {Id}");
        }

        public static BadRequestObjectResult ItemIntExists(string field, int Id)
        {
            return new BadRequestObjectResult($"{field} of {typeof(T).Name} already exists: {Id}");
        }

        public static BadRequestObjectResult ItemStringExists(string field, string value)
        {
            return new BadRequestObjectResult($"{field} of {typeof(T).Name} already exists: {value}");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KPIAPI.Data
{
    public class ApiResult<T>
    {
        public List<T> Data { get; private set; }
        private ApiResult(List<T> data)
        {
            Data = data;
        }

        public static async Task<ApiResult<T>> CreateAsync(IQueryable<T> source)
        {
            var data = await source.ToListAsync();
            return new ApiResult<T>(data);
        }

        //Will be used for preventing SQL injection
        public static bool IsValidProperty(
            string propertyName,
            bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
            propertyName,
            BindingFlags.IgnoreCase |
            BindingFlags.Public |
            BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException($"ERROR: Property '{propertyName}' does not exist.");
            return prop != null;
        }
    }
}

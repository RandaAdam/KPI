using KPIAPI.Data.Models;
using KPIAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace KPIAPI.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ApiResult<T>> GetAll();
        Task<T> GetById(int id);
        Task<ApiResult<T>> Find(Expression<Func<T, bool>> predicate);
        Task<bool> Update(T entity);
        Task<bool> Add(T entity);
        Task<bool> Delete(int id);
    }

}
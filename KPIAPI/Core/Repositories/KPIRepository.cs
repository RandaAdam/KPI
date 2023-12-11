using KPIAPI.Core.IRepositories;
using KPIAPI.Data;
using KPIAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KPIAPI.Core.Repositories
{
    public class KPIRepository:
        GenericRepository<KPI>, IKPIRepository
    {
        public KPIRepository(ApplicationDbContext context) 
            : base(context) 
        { 
        }

        public override async Task<ApiResult<KPI>> GetAll()
        {
            try
            {
                return await ApiResult<KPI>.CreateAsync(dbSet.AsNoTracking());
            }
            catch (Exception ex)
            {
                return await ApiResult<KPI>.CreateAsync(new List<KPI>().AsQueryable());
            }
        }

        public override async Task<bool> Update(KPI entity)
        {
            try
            {
                var existingKPI = await dbSet.Where(x => x.KPIIDNum == entity.KPIIDNum)
                                                    .FirstOrDefaultAsync();

                if (existingKPI == null)
                    return await Add(entity);

                existingKPI.KPIDescription = entity.KPIDescription;
                existingKPI.MeasurementUnit = entity.MeasurementUnit;
                existingKPI.TargetedValue = entity.TargetedValue;
                existingKPI.DepNo = entity.DepNo;
                context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.KPIIDNum == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;
                dbSet.Remove(exist);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

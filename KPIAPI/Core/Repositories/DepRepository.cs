using KPIAPI.Core.IRepositories;
using KPIAPI.Data;
using KPIAPI.Data.Models;

namespace KPIAPI.Core.Repositories
{
    public class DepRepository:
         GenericRepository<Department>, IDepRespository
    {
        public DepRepository(ApplicationDbContext context) :
            base(context) { }

        //complete implementing all needed functions
    }
}

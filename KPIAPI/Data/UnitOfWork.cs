using KPIAPI.Core.IConfiguration;
using KPIAPI.Core.IRepositories;
using KPIAPI.Core.Repositories;

namespace KPIAPI.Data
{
    public class UnitOfWork:IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        
        public IDepRespository Deps { get; private set; }
        public IKPIRepository Kpis { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Kpis = new KPIRepository(context);
            Deps = new DepRepository(context);
        }

        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

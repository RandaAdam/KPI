using KPIAPI.Core.IRepositories;

namespace KPIAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IKPIRepository Kpis { get; }
        IDepRespository Deps { get; }
        Task CompleteAsync();
    }
}

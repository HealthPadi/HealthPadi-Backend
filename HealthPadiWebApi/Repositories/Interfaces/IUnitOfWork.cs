namespace HealthPadiWebApi.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        IReportRepository Report { get; }
        Task CompleteAsync();
    }
}

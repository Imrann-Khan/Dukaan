namespace Dukaan.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IRepository<T> Repository<T>() where T : class;
    public Task<int> SaveChangesAsync();
}
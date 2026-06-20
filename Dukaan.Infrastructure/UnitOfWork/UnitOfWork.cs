using Dukaan.Application.Interfaces;
using Dukaan.Infrastructure.Data;
using Dukaan.Infrastructure.Repositories;

namespace Dukaan.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private readonly Dictionary<string, object> _repositories = new();
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;

        if(!_repositories.ContainsKey(type))
        {
            var repoType = typeof(Repository<>).MakeGenericType(typeof(T));
            var repoInstance = Activator.CreateInstance(repoType, _db);
            _repositories.Add(type, repoInstance!);
        }

        return (IRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}

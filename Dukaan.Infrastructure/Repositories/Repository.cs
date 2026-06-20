using Dukaan.Application.Interfaces;
using Dukaan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dukaan.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    public Repository(AppDbContext db) => _db = db;

    public async Task<List<T>> GetAllAsync() => await _db.Set<T>().ToListAsync<T>();
    public async Task<T?> GetByIdAsync(Guid id) => await _db.Set<T>().FindAsync(id);
    public async Task AddAsync(T entity) => await _db.Set<T>().AddAsync(entity);
    public void Remove(T entity) => _db.Set<T>().Remove(entity);
}
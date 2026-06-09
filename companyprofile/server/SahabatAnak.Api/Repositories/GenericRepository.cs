using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.Data;
using SahabatAnak.Api.Models;

namespace SahabatAnak.Api.Repositories;

public sealed class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _db;

    public GenericRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IQueryable<T> Query() => _db.Set<T>().AsQueryable();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task AddAsync(T entity, CancellationToken ct = default)
        => _db.Set<T>().AddAsync(entity, ct).AsTask();

    public void Update(T entity)
        => _db.Set<T>().Update(entity);

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
        _db.Set<T>().Update(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);

    public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => _db.Set<T>().CountAsync(predicate, ct);

    public Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, int skip, int take, CancellationToken ct = default)
        => _db.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync(ct);
}


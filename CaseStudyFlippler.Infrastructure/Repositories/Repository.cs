using CaseStudyFlippler.Application.Interfaces;
using CaseStudyFlippler.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Infrastructure.Repositories
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    protected readonly DataContext Context;

    public Repository(DataContext context)
    {
      Context = context;
    }

    public virtual async Task<TEntity> Get(int id)
    {
      return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
      return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return Context.Set<TEntity>().Where(predicate);
    }

    public virtual async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
      return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }

    public virtual void Add(TEntity entity)
    {
      Context.Set<TEntity>().Add(entity);
    }
    public virtual void Update(TEntity entity)
    {
      Context.Set<TEntity>().Update(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
      Context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
      Context.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
      Context.Set<TEntity>().RemoveRange(entities);
    }

    public virtual void Attach(TEntity entity)
    {
      Context.Attach(entity);
    }

    public int SaveChanges()
    {
      return Context.SaveChanges();
    }
    public async Task<int> SaveChangesAsync()
    {
      return await Context.SaveChangesAsync();
    }
  }
}

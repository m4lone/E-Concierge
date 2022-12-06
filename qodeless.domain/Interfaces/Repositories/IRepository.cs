using qodeless.domain.Model;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        bool Add(TEntity obj, bool bCommit = true);
        bool Upsert(TEntity obj, Func<TEntity, bool> exp, bool bCommit = true);
        bool Save(TEntity obj, bool bCommit = true);
        bool Update(TEntity obj, bool bCommit = true);
        bool ForceDelete(Guid id, bool bCommit = true);
        bool SoftDelete(TEntity obj);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllBy(Func<TEntity, bool> exp);
        TEntity GetBy(Func<TEntity, bool> exp);
        bool Any(Func<TEntity, bool> exp);
        bool None(Func<TEntity, bool> exp); 
        int SaveChanges();
        IQueryable<UserViewModel> GetAllUsers();
        UserViewModel GetUserById(string userId);
    }

    public enum EOrderByDirection
    {
        ASCENDING = 0,

        DESCENDING = 1
    }
}

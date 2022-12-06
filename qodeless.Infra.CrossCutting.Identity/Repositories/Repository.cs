using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;

namespace qodeless.domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        protected readonly ApplicationDbContext Db;

        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        public static void MainRepository(string[] args)
        {
            Logger.Info("==== Repository might have warnings or errors ==== ");

        }
        public IQueryable<TEntity> GetAllBy(Func<TEntity, bool> exp) => DbSet.Where(exp).AsQueryable().AsNoTracking();
        public TEntity GetBy(Func<TEntity, bool> exp) => DbSet.AsNoTracking().FirstOrDefault(exp);
        public TEntity GetById(Guid id) => DbSet.Find(id);

        /// <summary>
        /// Function Add run sql command "INSERT INTO TABLE"
        /// </summary>
        /// <param name="obj">Entity from this table</param>
        /// <param name="bCommit">True = Save Changes, False = Does not save on database </param>
        /// <returns></returns>
        public bool Add(TEntity obj, bool bCommit)
        {
            DbSet.Add(obj);
            Db.Entry(obj).State = EntityState.Added;
            return !bCommit || SaveChanges() > 0;
        }

        public bool AddNewer(TEntity obj, Func<TEntity, bool> exp, bool bCommit)
        {
            if (None(exp))
                return Add(obj, bCommit);

            return true;
        }

        /// <summary>
        /// Function Upsert run sql command "MERGE INTO TABLE"
        /// </summary>
        /// <param name="obj">Entity from this table</param>
        /// <param name="exp">Database Clause in Lambda expression</param>
        /// <param name="bCommit">True = Save Changes, False = Does not save on database</param>
        /// <returns></returns>
        public bool Upsert(TEntity obj, Func<TEntity, bool> exp, bool bCommit)
        {
            if (None(exp))
                return Add(obj, bCommit);
            else
                return Update(obj, bCommit);
        }
        public bool Any(Func<TEntity, bool> exp) => DbSet.AsNoTracking().Any(exp);
        public bool None(Func<TEntity, bool> exp) => !DbSet.AsNoTracking().Any(exp);
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
        public IQueryable<TEntity> GetAll() => DbSet.AsNoTracking();
        public bool ForceDelete(Guid id, bool bCommit)
        {
            var obj = DbSet.Find(id);
            DbSet.Remove(obj);
            Db.Entry(obj).State = EntityState.Deleted;
            return !bCommit || SaveChanges() > 0;
        }
        public bool SoftDelete(TEntity obj)
        {
            var entity = obj as Entity;
            if (entity == null) return false;
            if (entity.Excluded && entity.DeletedAt < DateTime.Now) return false;
            entity.Excluded = true;
            entity.DeletedAt = DateTime.Now;
            DbSet.Update(obj);
            var result = SaveChanges() > 0;
            if (!result) return false;
            return result;
            //ForceDelete(entity.Id, true);
            //entity.Delete();
        }
        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return 0;
            }
        }
        public bool Update(TEntity obj, bool bCommit)
        {
            try
            {
                var entity = obj as Entity;
                if (entity == null) return false;

                Db.Entry(obj).State = EntityState.Detached;
                Db.SaveChanges();
                Db.Entry(obj).State = EntityState.Modified;
                entity.Update();
                entity.UpdatedAt = DateTime.Now;
                DbSet.Update(obj);

                if (bCommit)
                    SaveChanges();

                Db.Entry(obj).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                //TODO: Tratar Affected Rows == 0
            }
            return true;
        }
        public bool Save(TEntity obj, bool bCommit = true)
        {
            var entity = obj as Entity;
            if (Guid.Empty == entity.Id)
                Add(obj, false);
            else
                Update(obj, false);

            return !bCommit || SaveChanges() > 0;
        }
        public IQueryable<UserViewModel> GetAllUsers()
        {
            return Db.Users.Select(x => new UserViewModel { Email = x.Email, Id = x.Id, NickName = x.NickName}).AsNoTracking();
        }
        public UserViewModel GetUserById(string userId)
        {
            return Db.Users.Where(u => u.Id.Equals(userId)).Select(x => new UserViewModel { Email = x.Email, Id = x.Id, NickName = x.NickName }).AsNoTracking().FirstOrDefault();
        }
    }
}

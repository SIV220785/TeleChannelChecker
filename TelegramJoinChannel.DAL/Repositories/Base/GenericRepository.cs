using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TelegramJoinChannel.DAL.Repositories.Base
{
    internal class GenericRepository<T> : IRepository<T> where T : class
    {
        private bool _disposed;
        protected DbContext Context { get; }

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }
        public virtual T GetIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            return queryable;
        }
        public virtual T Add(T t)
        {
            Context.Set<T>().Add(t);
            Context.SaveChanges();
            return t;
        }
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().SingleOrDefault(match);
        }
        public virtual T FindIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = Context.Set<T>().Where(match);

            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable.SingleOrDefault();
        }
        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().Where(match);
        }
        public virtual IQueryable<T> FindAllIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = Context.Set<T>().Where(match);

            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }
        public virtual int Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChanges();
        }
        public virtual int DeleteBy(int id)
        {
            var entity = Context.Set<T>().Find(id);
            if (entity != null)
            {
                Context.Set<T>().Remove(entity);
                return Context.SaveChanges();
            }

            return 0;
        }
        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = Context.Set<T>().Find(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(t);
                Context.SaveChanges();
            }
            return exist;
        }
        public virtual int Count()
        {
            return Context.Set<T>().Count();
        }
        public virtual void Save()
        {

            Context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

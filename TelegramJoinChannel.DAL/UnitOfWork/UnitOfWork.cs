using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

using TelegramJoinChannel.DAL.Contexts;
using TelegramJoinChannel.DAL.Repositories.Base;
using TelegramJoinChannel.DAL.UnitOfWork.Base;


namespace TelegramJoinChannel.DAL.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {

        private Hashtable _repositories;
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
            _dbContext.Configuration.LazyLoadingEnabled = false;
        }


        public void Commit()
        {
            _dbContext.SaveChanges();
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type)) return (GenericRepository<TEntity>)_repositories[type];

            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

            _repositories.Add(type, repositoryInstance);

            return (GenericRepository<TEntity>)_repositories[type];
        }

        #region GC
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    _dbContext.Dispose();
                    _repositories.Clear();
                }
                else
                {
                    _dbContext.Dispose();
                }

            _disposed = true;
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
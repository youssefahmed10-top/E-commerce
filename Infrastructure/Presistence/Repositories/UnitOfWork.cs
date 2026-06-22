using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        //private Dictionary<string, object> _repositories = new();
        //private Dictionary<string, object> _repositories = new Dictionary<string, object>();
        //private Dictionary<string, object> _repositories;
        private ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenericRepository<TEntity, TKey>)_repositories.
            GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_dbContext));

            //Dictionary : [key,Value]
            //Key : Name of Entity
            //Value : Object from Genaric Repo

            //var key = typeof(TEntity).Name;
            //if (!_repositories.ContainsKey(key))
            //    _repositories[key] = new GenericRepository<TEntity, TKey>(_dbContext);

            //return (IGenericRepository<TEntity, TKey>)_repositories[key];


        

        public async Task<int> SaveChanges()
            => await _dbContext.SaveChangesAsync();
    }
}

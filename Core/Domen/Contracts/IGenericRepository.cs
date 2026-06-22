using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey>where TEntity : BaseEntity<TKey>
    {
        //GetById()
        //GetAll()
        //Add()
        //Update
        //Delete
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        Task<TEntity?> GetByIdAsync(TKey id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task AddAsync(TEntity entity);

        #region Specification:
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,TKey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
        #endregion

    }
}

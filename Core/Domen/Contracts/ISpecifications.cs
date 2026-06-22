using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity :BaseEntity<TKey>
    {
        //Signature for proprty [Expression == Where]
        public Expression<Func<TEntity,bool>>? Criteria { get;}
        //Signature for Proprty [Expression == Include]
        public List<Expression<Func<TEntity,object>>> IncludeExpression { get;}

        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }

        public int Take { get; } //take = PageSize = hoa many product in thos page
        public int Skip { get; } //Skip = Pageindex namber of page
        public bool IsPaginated { get; }
    }
}

using System.Linq.Expressions;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> :
        ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Criteria
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

        //Set Expression From Cotr;
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion

        #region include

        //Set Expression From Method;
        //AddInclude(P=>P.ProductBarnd)
        //AddInclude(P=>P.ProductType)
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = new();
        protected void AddInclude(Expression<Func<TEntity, object>> includeExprission)
        {
            IncludeExpression.Add(includeExprission);
        }
        #endregion

        #region Sorting [OrderBy,OrderByDescending]
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }


        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExprssion) => OrderBy = OrderByExprssion;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescendingExpression)
            => OrderByDescending = OrderByDescendingExpression;


        #endregion

        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int PageSize, int PageIndex) 
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex-1)* PageSize;
        }
        #endregion
    }
}

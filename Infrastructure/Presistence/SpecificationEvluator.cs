using Domain.Contracts;
using Domain.Entities;

namespace Presistence
{
    internal static class SpecificationEvluator
    {
        public static IQueryable<TEntity> Create<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity:BaseEntity<Tkey> 
        {
            var Query = inputQuery;

            if (specifications.Criteria is not null) 
            {
                Query = Query.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }

            if (specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            }

            if(specifications.IncludeExpression is not null && specifications.IncludeExpression.Count > 0) 
            {
                //foreach(var epx in specifications.IncludeExpression)
                //{
                //    Query = Query.Include(epx);
                //}

                Query = specifications.IncludeExpression.Aggregate(Query, (CurrentQuery, Expression) => CurrentQuery.Include(Expression));
            }


            if (specifications.IsPaginated)
            { 
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }

            return Query;
        }
    }
}

using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Where
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression )
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        #endregion

        #region Sorting (OrderBy - OrderByDesc) 
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp)
        {
            OrderByDescending = orderByDescExp;
        }
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpressions)
        {
            IncludeExpressions.Add(includeExpressions);
        }
        #endregion

        #region Pagination
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; set; }

        public void ApplyPagination(int PageSize , int PageIndex)
        {
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
            IsPaginated = true;
        }
        #endregion
    }
}

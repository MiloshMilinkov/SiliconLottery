using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : iSpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification(){}

        public Expression<Func<T, bool>> Criteria { get;}
        public List<Expression<Func<T, object>>> Includes { get; }= new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBY {get; private set;}

        public Expression<Func<T, object>> OrderBYDescending {get; private set;}

        protected void  AddInclude(Expression<Func<T,object>> includeExpression){
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T,object>> orderByExpression){
            OrderBY=orderByExpression;
        }
        
        protected void AddOrderByDescending(Expression<Func<T,object>> orderByDesc){
            OrderBYDescending=orderByDesc;
        }
    }
}
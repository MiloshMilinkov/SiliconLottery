using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface iSpecification<T>
    {
        Expression<Func<T,bool>> Criteria{get;}
        List<Expression<Func<T,object>>> Includes{get;}
        Expression<Func<T,object>> OrderBY {get;}
        Expression<Func<T,object>> OrderBYDescending {get;}
    }
}
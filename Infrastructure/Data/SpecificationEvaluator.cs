using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, iSpecification<TEntity> spec){
            var query=inputQuery;
            if(spec.Criteria!=null){
                query=query.Where(spec.Criteria);
            }
            if(spec.OrderBY!=null){
                query=query.OrderBy(spec.OrderBY);
            }
            if(spec.OrderBYDescending!=null){
                query=query.OrderByDescending(spec.OrderBYDescending);
            }
            query=spec.Includes.Aggregate(query,(current,include)=>current.Include(include));
            return query;
        }
    }
}
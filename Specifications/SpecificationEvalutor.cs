using GuardingChild.Models;
using Microsoft.EntityFrameworkCore;

namespace GuardingChild.Specifications;

public static class SpecificationEvalutor<T> where T:BaseModel
{
    //Fun to build query in dynamic way
    public static IQueryable<T> BuildQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var Query = inputQuery; //_dbContext.Set<T>()
        if (spec.Criteria is not null)
        {
            Query = Query.Where(spec.Criteria); //_dbContext.Set<T>().where(P=>P.Id==id)
        }


        Query = spec.Includes.Aggregate(Query, (CurrentQuery, NextIncludeExpression) => CurrentQuery.Include(NextIncludeExpression));
        return Query;
    }
}
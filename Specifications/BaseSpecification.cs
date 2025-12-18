using System.Linq.Expressions;
using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class BaseSpecification<T>:ISpecification<T> where T:BaseModel
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPagingEnabled { get; set; }

    public BaseSpecification()
    {
        // Includes = new List<Expression<Func<T, object>>>();
    }

    //Get by id
    public BaseSpecification(Expression<Func<T, bool>>  criteriaExp)
    {
        Criteria = criteriaExp;
        // Includes = new List<Expression<Func<T, object>>>();
    }

    public void ApplyPagination(int skip, int take)
    {
        Skip =  skip;
        Take = take;
        IsPagingEnabled = true; // when apply spec for count, i don't need pagination, so init IsPagingEnabled is false
    }
}
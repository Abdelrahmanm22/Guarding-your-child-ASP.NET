using System.Linq.Expressions;
using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class BaseSpecification<T>:ISpecification<T> where T:BaseModel
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T,object>> OrderBy { get; set; }
    public Expression<Func<T,object>> OrderByDesc { get; set; }
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
    public void AddOrderBy(Expression<Func<T, object>> orderByExp)
    {
        OrderBy = orderByExp;
    }

    public void AddOrderByDesc(Expression<Func<T, object>> orderByExp)
    {
        OrderByDesc = orderByExp;
    }
}
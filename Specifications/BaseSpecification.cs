using System.Linq.Expressions;
using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class BaseSpecification<T>:ISpecification<T> where T:BaseModel
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

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

}
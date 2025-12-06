using System.Linq.Expressions;
using GuardingChild.Models;

namespace GuardingChild.Specifications;

public interface ISpecification<T> where T :BaseModel
{
    //prop sign for (Where(p=>p.Id == id))
    public Expression<Func<T,bool>> Criteria { get; set; }
    //prop sign for (.Include(P => P.ProductBrand).Include(P => P.ProductType)) => list of includes
    public List<Expression<Func<T, object>>> Includes { get; set; }

}
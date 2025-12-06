using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class KidWithGuardingSpecification : BaseSpecification<Kid>
{
    public KidWithGuardingSpecification():base()
    {
        Includes.Add(k=>k.Guardian);
    }

    public KidWithGuardingSpecification(int id):base(k => k.Id == id)
    {
        Includes.Add(k=>k.Guardian);
    }
}
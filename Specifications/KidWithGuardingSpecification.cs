using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class KidWithGuardingSpecification : BaseSpecification<Kid>
{
    public KidWithGuardingSpecification(string? ssn)
        :base(k=>
            (string.IsNullOrEmpty(ssn) || k.SSN.Contains(ssn))
        )
    {
        Includes.Add(k=>k.Guardian);
    }

    public KidWithGuardingSpecification(int id):base(k => k.Id == id)
    {
        Includes.Add(k=>k.Guardian);
    }
}
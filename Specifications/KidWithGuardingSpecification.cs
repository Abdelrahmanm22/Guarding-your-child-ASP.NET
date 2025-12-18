using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class KidWithGuardingSpecification : BaseSpecification<Kid>
{
    public KidWithGuardingSpecification(KidSpecParams kidSpec)
        :base(k=>
            (string.IsNullOrEmpty(kidSpec.Ssn) || k.SSN.ToLower().Contains(kidSpec.Ssn))
        )
    {
        Includes.Add(k=>k.Guardian);
        ApplyPagination(kidSpec.PageSize * (kidSpec.PageIndex-1),kidSpec.PageSize);
    }

    public KidWithGuardingSpecification(int id):base(k => k.Id == id)
    {
        Includes.Add(k=>k.Guardian);
    }
}
using GuardingChild.Models;

namespace GuardingChild.Specifications;

public class KidWithFiltrationForCountAsync : BaseSpecification<Kid>
{
    public KidWithFiltrationForCountAsync(KidSpecParams kidSpec)
        :base(k=>
            (string.IsNullOrEmpty(kidSpec.Ssn) || k.SSN.ToLower().Contains(kidSpec.Ssn))
        )
    {

    }
}
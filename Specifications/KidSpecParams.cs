namespace GuardingChild.Specifications;

public class KidSpecParams
{
    private int pageSize = 5;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > 10 ? 10 : value; }
    }

    public int PageIndex { get; set; } = 1;
    private string? ssn;
    public string? Ssn
    {
        get { return ssn; }
        set { ssn = value.ToLower(); }
    }
}
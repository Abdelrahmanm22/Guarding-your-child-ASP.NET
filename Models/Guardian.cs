namespace GuardingChild.Models;

public class Guardian : BaseModel
{
    public string SSN_Father { get; set; }
    public string Father_Name { get; set; }
    public string SSN_Mother { get; set; }
    public string Mother_Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public ICollection<Kid> Kids { get; set; } = new HashSet<Kid>();
}
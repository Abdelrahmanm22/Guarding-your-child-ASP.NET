using GuardingChild.Enums;

namespace GuardingChild.DTOs;

public class KidToReturnDto
{
    public int Id { get; set; }
    public long Index { get; set; }
    public string SSN { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
}
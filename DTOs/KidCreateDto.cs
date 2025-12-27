using GuardingChild.Enums;
using Microsoft.AspNetCore.Http;

namespace GuardingChild.DTOs;

public class KidCreateDto
{
    public string SSN { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public int? GuardianId { get; set; }
    public GuardianCreateDto? Guardian { get; set; }
    public IFormFile Image { get; set; }
}

using System.Text.Json.Serialization;
using GuardingChild.Enums;

namespace GuardingChild.DTOs;

public class KidToReturnDto
{
    public int Id { get; set; }
    public long Index { get; set; }
    public string SSN { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public int GuardianId { get; set; }
    public string SSN_Father { get; set; }
    public string Father_Name { get; set; }
    public string SSN_Mother { get; set; }
    public string Mother_Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}
using GuardingChild.DTOs;
using GuardingChild.Models;

namespace GuardingChild.Services.Interfaces;

public interface IKidService
{
    Task<(Kid? Kid, string? ErrorMessage)> AddKidAsync(KidCreateDto model);
    Task<(string? Message, string? ErrorMessage)> UpdateKidAsync(int id, KidUpdateDto model);
    Task<(Kid? Kid, string? ErrorMessage)> SearchAsync(KidSearchDto model);
}

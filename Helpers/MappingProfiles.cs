using AutoMapper;
using GuardingChild.DTOs;
using GuardingChild.Models;

namespace GuardingChild.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Kid, KidToReturnDto>();
    }
}
using AutoMapper;
using GuardingChild.DTOs;
using GuardingChild.Models;

namespace GuardingChild.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Kid, KidToReturnDto>()
            .ForMember(dest=>dest.SSN_Father,opt=>opt.MapFrom(src=>src.Guardian.SSN_Father))
            .ForMember(dest=>dest.Father_Name,opt=>opt.MapFrom(src=>src.Guardian.Father_Name))
            .ForMember(dest=>dest.SSN_Mother,opt=>opt.MapFrom(src=>src.Guardian.SSN_Mother))
            .ForMember(dest=>dest.Mother_Name,opt=>opt.MapFrom(src=>src.Guardian.Mother_Name))
            .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=>src.Guardian.Address))
            .ForMember(dest=>dest.Phone,opt=>opt.MapFrom(src=>src.Guardian.Phone));
    }
}
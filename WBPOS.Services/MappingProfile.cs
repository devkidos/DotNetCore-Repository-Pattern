using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WBPOS.Entities;
using WBPOS.ViewModel;

namespace WBPOS.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Country, VMCountry>().ReverseMap(); 
            CreateMap<State, VMState>().ReverseMap();  
            CreateMap<User, VMUser>().ReverseMap();
            CreateMap<User, VMUsers>().ReverseMap(); 

            CreateMap<ddlList, Country>()
                .ForMember(dest => dest.countryId, act => act.MapFrom(src => src.ValueText))
                .ForMember(dest => dest.countryName, act => act.MapFrom(src => src.DispalyText)).ReverseMap();
            CreateMap<ddlList, State>()
               .ForMember(dest => dest.stateId, act => act.MapFrom(src => src.ValueText))
               .ForMember(dest => dest.stateName, act => act.MapFrom(src => src.DispalyText)).ReverseMap();
           
        }
    }
}
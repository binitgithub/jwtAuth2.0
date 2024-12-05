using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jwtAuth2._0.DTOs;
using jwtAuth2._0.Models;

namespace jwtAuth2._0.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<User, RegisterUserDto>().ReverseMap(); 
        }
        
    }
}
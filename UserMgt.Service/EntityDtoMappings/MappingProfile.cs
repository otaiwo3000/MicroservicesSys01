using AutoMapper;
using System;
using UserMgt.Service.DtoModels;
using UserMgt.Shared.Entities;

namespace UserMgt.Service.EntityDtoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {          
            CreateMap<Users, UsersModel>();
            CreateMap<UsersCreateModel, Users>();
            CreateMap<UsersUpdateModel, Users>();
          
        }
    }
}

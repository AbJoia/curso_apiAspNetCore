using AutoMapper;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Entities;

namespace src.Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>()
                .ReverseMap();
            
            CreateMap<UserDTOCreateResult, UserEntity>()
                .ReverseMap();
            
            CreateMap<UserDTOUpdateResult, UserEntity>()
                .ReverseMap();
        }
    }
}
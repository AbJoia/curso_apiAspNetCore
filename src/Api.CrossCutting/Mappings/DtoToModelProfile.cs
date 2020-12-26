using AutoMapper;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Models;

namespace src.Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDTO>()
                .ReverseMap();
            CreateMap<UserModel, UserDTOCreate>()
                .ReverseMap();
            CreateMap<UserModel, UserDTOUpdate>()
                .ReverseMap();            
        }
    }
}
using AutoMapper;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Entities;

namespace src.Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {   
            #region User
            CreateMap<UserDTO, UserEntity>()
                .ReverseMap();            
            CreateMap<UserDTOCreateResult, UserEntity>()
                .ReverseMap();            
            CreateMap<UserDTOUpdateResult, UserEntity>()
                .ReverseMap();
            #endregion

            #region UF
            CreateMap<UfDto, UfEntity>()
                .ReverseMap();
            #endregion

            #region Municipio
            CreateMap<MunicipioDto, MunicipioEntity>()
                .ReverseMap();
            CreateMap<MunicipioDtoCompleto, MunicipioEntity>()
                .ReverseMap();           
            CreateMap<MunicipioDtoCreateResult, MunicipioEntity>()
                .ReverseMap();            
            CreateMap<MunicipioDtoUpdateResult, MunicipioEntity>()
                .ReverseMap();
            #endregion
            
            #region CEP
            CreateMap<CepDto, CepEntity>()
                .ReverseMap();            
            CreateMap<CepDtoCreateResult, CepEntity>()
                .ReverseMap();            
            CreateMap<CepDtoUpdateResult, CepEntity>()
                .ReverseMap();
            #endregion
        }
    }
}
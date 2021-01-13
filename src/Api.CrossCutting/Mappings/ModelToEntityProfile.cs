using AutoMapper;
using src.Api.Domain.Entities;
using src.Api.Domain.Models;

namespace src.Api.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {   
            #region User        
            CreateMap<UserEntity, UserModel>()
                .ReverseMap(); 
            #endregion
            
            #region UF
            CreateMap<UfEntity, UfModel>()
                .ReverseMap(); 
            #endregion
            
            #region Municipio
            CreateMap<MunicipioEntity, MunicipioModel>()
                .ReverseMap();
            #endregion
            
            #region CEP
            CreateMap<CepEntity, CepModel>()
                .ReverseMap();
            #endregion
        }
    }
}
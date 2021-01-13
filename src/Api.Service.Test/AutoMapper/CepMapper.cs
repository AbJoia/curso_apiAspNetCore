using System;
using Api.Service.Test;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Services.Cep;
using src.Api.Domain.Models;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possivel Mapear os Modelos Cep")]
        public void E_Possivel_Mapear_Modelos_Cep()
        {
            var cepModel = new CepModel()
            {
                Id = Guid.NewGuid(),
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(100, 999).ToString(),
                MunicipioId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddHours(2.0),
            };

            //Model => Entity
            var entity = Mapper.Map<CepEntity>(cepModel);
            Assert.NotNull(entity); 
            Assert.Equal(entity.Id, cepModel.Id);            
            Assert.Equal(entity.Cep, cepModel.Cep);
            Assert.Equal(entity.Logradouro, cepModel.Logradouro);
            Assert.Equal(entity.Numero, cepModel.Numero);
            Assert.Equal(entity.MunicipioId, cepModel.MunicipioId);
            Assert.Equal(entity.CreateAt, cepModel.CreateAt);
            Assert.Equal(entity.UpdateAt, cepModel.UpdateAt);                   
            Assert.IsType<CepEntity>(entity);

            //Entity => Dto            
            entity.Municipio = new MunicipioEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Name.FullName(),
                    UfId = Guid.NewGuid(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),                 
                    Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.City(),
                        Sigla = Faker.Address.City().Substring(1, 3),                       
                    }                   
                };

            var cepDto = Mapper.Map<CepDto>(entity);
            Assert.NotNull(cepDto); 
            Assert.Equal(cepDto.Id, entity.Id);            
            Assert.Equal(cepDto.Cep, entity.Cep);
            Assert.Equal(cepDto.Logradouro, entity.Logradouro);
            Assert.Equal(cepDto.Numero, entity.Numero);            
            Assert.Equal(cepDto.MunicipioId, entity.MunicipioId);  
            Assert.Equal(cepDto.Municipio.Id, entity.Municipio.Id);   
            Assert.Equal(cepDto.Municipio.Nome, entity.Municipio.Nome);
            Assert.Equal(cepDto.Municipio.UfId, entity.Municipio.UfId);
            Assert.Equal(cepDto.Municipio.CodIBGE, entity.Municipio.CodIBGE); 
            Assert.Equal(cepDto.Municipio.Uf.Id, entity.Municipio.Uf.Id); 
            Assert.Equal(cepDto.Municipio.Uf.Nome, entity.Municipio.Uf.Nome); 
            Assert.Equal(cepDto.Municipio.Uf.Sigla, entity.Municipio.Uf.Sigla);                                               
            Assert.IsType<CepDto>(cepDto);

            var cepDtoCreateResult = Mapper.Map<CepDtoCreateResult>(entity);
            Assert.NotNull(cepDtoCreateResult); 
            Assert.Equal(cepDtoCreateResult.Id, entity.Id);            
            Assert.Equal(cepDtoCreateResult.Cep, entity.Cep);
            Assert.Equal(cepDtoCreateResult.Logradouro, entity.Logradouro);
            Assert.Equal(cepDtoCreateResult.Numero, entity.Numero);
            Assert.Equal(cepDtoCreateResult.MunicipioId, entity.MunicipioId);
            Assert.Equal(cepDtoCreateResult.CreateAt, entity.CreateAt);                              
            Assert.IsType<CepDtoCreateResult>(cepDtoCreateResult);

            var cepDtoUpdateResult = Mapper.Map<CepDtoUpdateResult>(entity);
            Assert.NotNull(cepDtoUpdateResult); 
            Assert.Equal(cepDtoUpdateResult.Id, entity.Id);            
            Assert.Equal(cepDtoUpdateResult.Cep, entity.Cep);
            Assert.Equal(cepDtoUpdateResult.Logradouro, entity.Logradouro);
            Assert.Equal(cepDtoUpdateResult.Numero, entity.Numero);
            Assert.Equal(cepDtoUpdateResult.MunicipioId, entity.MunicipioId);
            Assert.Equal(cepDtoUpdateResult.UpdateAt, entity.UpdateAt);                              
            Assert.IsType<CepDtoUpdateResult>(cepDtoUpdateResult);

            //Dto => Model
            cepModel = Mapper.Map<CepModel>(cepDto);
            Assert.NotNull(cepModel); 
            Assert.Equal(cepModel.Id, cepDto.Id);            
            Assert.Equal(cepModel.Cep, cepDto.Cep);
            Assert.Equal(cepModel.Logradouro, cepDto.Logradouro);
            Assert.Equal(cepModel.Numero, cepDto.Numero);
            Assert.Equal(cepModel.MunicipioId, cepDto.MunicipioId);                                                     
            Assert.IsType<CepModel>(cepModel);

            cepDto.Numero = "";
            cepModel = Mapper.Map<CepModel>(cepDto);           
            Assert.Equal("S/N", cepModel.Numero);
          

            var cepCreate = new CepDtoCreate
            {
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetName(),
                MunicipioId = Guid.NewGuid(),
                Numero = Faker.RandomNumber.Next(100, 999).ToString(),                
            };

            cepModel = Mapper.Map<CepModel>(cepCreate);
            Assert.NotNull(cepModel);                       
            Assert.Equal(cepModel.Cep, cepCreate.Cep);
            Assert.Equal(cepModel.Logradouro, cepCreate.Logradouro);
            Assert.Equal(cepModel.Numero, cepCreate.Numero);
            Assert.Equal(cepModel.MunicipioId, cepCreate.MunicipioId);                                                     
            Assert.IsType<CepModel>(cepModel);

            var cepCreateUpdate = new CepDtoUpdate
            {
                Id = Guid.NewGuid(),
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetName(),
                MunicipioId = Guid.NewGuid(),
                Numero = Faker.RandomNumber.Next(100, 999).ToString(),                
            };

            cepModel = Mapper.Map<CepModel>(cepCreateUpdate);
            Assert.NotNull(cepModel);
            Assert.Equal(cepModel.Id, cepCreateUpdate.Id);                       
            Assert.Equal(cepModel.Cep, cepCreateUpdate.Cep);
            Assert.Equal(cepModel.Logradouro, cepCreateUpdate.Logradouro);
            Assert.Equal(cepModel.Numero, cepCreateUpdate.Numero);
            Assert.Equal(cepModel.MunicipioId, cepCreateUpdate.MunicipioId);                                                     
            Assert.IsType<CepModel>(cepModel);
        
        }
    }
}
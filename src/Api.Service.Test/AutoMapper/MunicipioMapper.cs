using System;
using System.Collections.Generic;
using System.Linq;
using Api.Service.Test;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Entities;
using src.Api.Domain.Models;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possivel Mapear os Modelos Municipio")]
        public void E_Possivel_Mapear_os_Modelos_Municipio()
        {
            var municipioModel = new MunicipioModel()
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                CreateAt = DateTime.UtcNow,
                UfId = Guid.NewGuid(),
                UpdateAt = DateTime.UtcNow.AddHours(2.0)
            };

            List<MunicipioEntity> municipios = new List<MunicipioEntity>();
            for(int i = 0; i < new Random().Next(5, 10); i++)
            {
                municipios.Add(
                    new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Name.FullName(),
                        CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                        CreateAt = DateTime.UtcNow,
                        UfId = Guid.NewGuid(),
                        UpdateAt = DateTime.UtcNow.AddHours(2.0),
                        Uf = new UfEntity
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)                        
                        }
                    }    
                );
            }

            //Model => Entity
            var entity = Mapper.Map<MunicipioEntity>(municipioModel);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, municipioModel.Id);
            Assert.Equal(entity.Nome, municipioModel.Nome);
            Assert.Equal(entity.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(entity.CreateAt, municipioModel.CreateAt);
            Assert.Equal(entity.UpdateAt, municipioModel.UpdateAt);
            Assert.Equal(entity.UfId, municipioModel.UfId);            
            Assert.IsType<MunicipioEntity>(entity);

            //Entity => Dto
            var municipioDto = Mapper.Map<MunicipioDto>(entity);
            Assert.NotNull(municipioDto);
            Assert.Equal(municipioDto.Id, entity.Id);
            Assert.Equal(municipioDto.Nome, entity.Nome);
            Assert.Equal(municipioDto.CodIBGE, entity.CodIBGE);            
            Assert.Equal(municipioDto.UfId, entity.UfId);            
            Assert.IsType<MunicipioDto>(municipioDto);

            var listMunicipios = Mapper.Map<List<MunicipioDto>>(municipios);
            Assert.NotNull(listMunicipios);
            Assert.Equal(listMunicipios.Count(), municipios.Count());
            for(int i = 0; i < municipios.Count(); i++)
            {
                Assert.Equal(listMunicipios[i].Id, municipios[i].Id);
                Assert.Equal(listMunicipios[i].Nome, municipios[i].Nome);
                Assert.Equal(listMunicipios[i].CodIBGE, municipios[i].CodIBGE);                
                Assert.Equal(listMunicipios[i].UfId, municipios[i].UfId); 
            }                       
            Assert.IsType<MunicipioEntity>(entity);

            
            var municipioDtoCompleto = Mapper.Map<MunicipioDtoCompleto>(municipios.FirstOrDefault());
            Assert.NotNull(municipioDtoCompleto);
            Assert.Equal(municipios.FirstOrDefault().Id, municipioDtoCompleto.Id);
            Assert.Equal(municipios.FirstOrDefault().Nome, municipioDtoCompleto.Nome);
            Assert.Equal(municipios.FirstOrDefault().CodIBGE, municipioDtoCompleto.CodIBGE);            
            Assert.Equal(municipios.FirstOrDefault().UfId, municipioDtoCompleto.UfId);
            Assert.Equal(municipios.FirstOrDefault().Uf.Id, municipioDtoCompleto.Uf.Id);
            Assert.Equal(municipios.FirstOrDefault().Uf.Nome, municipioDtoCompleto.Uf.Nome);
            Assert.Equal(municipios.FirstOrDefault().Uf.Sigla, municipioDtoCompleto.Uf.Sigla);
            Assert.IsType<MunicipioDtoCompleto>(municipioDtoCompleto);

            var municipioDtoCreateResult = Mapper.Map<MunicipioDtoCreateResult>(entity);
            Assert.NotNull(municipioDtoCreateResult);
            Assert.Equal(entity.Id, municipioDtoCreateResult.Id);
            Assert.Equal(entity.Nome, municipioDtoCreateResult.Nome);
            Assert.Equal(entity.CodIBGE, municipioDtoCreateResult.CodIBGE);            
            Assert.Equal(entity.UfId, municipioDtoCreateResult.UfId);
            Assert.Equal(entity.CreateAt, municipioDtoCreateResult.CreateAt);
            Assert.IsType<MunicipioDtoCreateResult>(municipioDtoCreateResult);

            var municipioDtoUpdateResult = Mapper.Map<MunicipioDtoUpdateResult>(entity);
            Assert.NotNull(municipioDtoUpdateResult);
            Assert.Equal(entity.Id, municipioDtoUpdateResult.Id);
            Assert.Equal(entity.Nome, municipioDtoUpdateResult.Nome);
            Assert.Equal(entity.CodIBGE, municipioDtoUpdateResult.CodIBGE);            
            Assert.Equal(entity.UfId, municipioDtoUpdateResult.UfId);
            Assert.Equal(entity.UpdateAt, municipioDtoUpdateResult.UpdateAt);
            Assert.IsType<MunicipioDtoUpdateResult>(municipioDtoUpdateResult);

            //Dto => Model
            var municipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid()
            };

            municipioModel = Mapper.Map<MunicipioModel>(municipioDtoCreate);
            Assert.NotNull(municipioModel);            
            Assert.Equal(municipioModel.Nome, municipioDtoCreate.Nome);
            Assert.Equal(municipioModel.CodIBGE, municipioDtoCreate.CodIBGE);            
            Assert.Equal(municipioModel.UfId, municipioDtoCreate.UfId);            
            Assert.IsType<MunicipioModel>(municipioModel);

            var municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid()
            };

            municipioModel = Mapper.Map<MunicipioModel>(municipioDtoUpdate);
            Assert.NotNull(municipioModel);
            Assert.True(municipioModel.Id != Guid.Empty || municipioModel.Id != null);            
            Assert.Equal(municipioModel.Nome, municipioDtoUpdate.Nome);
            Assert.Equal(municipioModel.CodIBGE, municipioDtoUpdate.CodIBGE);            
            Assert.Equal(municipioModel.UfId, municipioDtoUpdate.UfId);            
            Assert.IsType<MunicipioModel>(municipioModel);

            municipioModel = Mapper.Map<MunicipioModel>(municipioDto);
            Assert.NotNull(municipioModel);
            Assert.True(municipioModel.Id != Guid.Empty || municipioModel.Id != null);            
            Assert.Equal(municipioModel.Nome, municipioDto.Nome);
            Assert.Equal(municipioModel.CodIBGE, municipioDto.CodIBGE);            
            Assert.Equal(municipioModel.UfId, municipioDto.UfId);            
            Assert.IsType<MunicipioModel>(municipioModel);
        }        
    }
}
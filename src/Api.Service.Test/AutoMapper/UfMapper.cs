using System;
using System.Collections.Generic;
using System.Linq;
using Api.Service.Test;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Services.Uf;
using src.Api.Domain.Models;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possivel Mapear os Modelos de UF")]
        public void E_Possivel_Mapear_os_Modelos_Uf()
        {
            var modelUf = new UfModel()
            {
                Id = Guid.NewGuid(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                Nome = Faker.Address.UsState(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddHours(2.0)                
            };

            List<UfEntity> listEntity = new List<UfEntity>();

            for(int i=0; i<new Random().Next(5, 10); i++)
            {
                listEntity.Add(
                    new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Sigla = Faker.Address.UsState().Substring(1, 3),
                        Nome = Faker.Address.UsState(),
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow.AddHours(2.0)
                    }    
                );
            }

            //Model => Entity
            var entity = Mapper.Map<UfEntity>(modelUf);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, modelUf.Id);
            Assert.Equal(entity.Nome, modelUf.Nome);
            Assert.Equal(entity.Sigla, modelUf.Sigla);
            Assert.Equal(entity.CreateAt, modelUf.CreateAt);
            Assert.Equal(entity.UpdateAt, modelUf.UpdateAt);
            Assert.IsType<UfEntity>(entity);

            //Entity => UfDto
            var ufDto = Mapper.Map<UfDto>(entity);
            Assert.NotNull(ufDto);
            Assert.Equal(ufDto.Id, entity.Id);
            Assert.Equal(ufDto.Nome, entity.Nome);
            Assert.Equal(ufDto.Sigla, entity.Sigla);            
            Assert.IsType<UfDto>(ufDto);

            //ListEntity => ListUfDto
            var listUfDto = Mapper.Map<List<UfDto>>(listEntity);
            Assert.NotNull(listUfDto);
            for(int i=0; i < listUfDto.Count(); i++)
            {
                Assert.Equal(listUfDto[i].Id, listEntity[i].Id);
                Assert.Equal(listUfDto[i].Nome, listEntity[i].Nome);
                Assert.Equal(listUfDto[i].Sigla, listEntity[i].Sigla);
            }
            Assert.IsType<List<UfDto>>(listUfDto);

            //Dto => Model
            modelUf = Mapper.Map<UfModel>(ufDto);
            Assert.NotNull(modelUf);
            Assert.Equal(ufDto.Id, modelUf.Id);
            Assert.Equal(ufDto.Nome, modelUf.Nome);
            Assert.Equal(ufDto.Sigla, modelUf.Sigla);            
            Assert.IsType<UfModel>(modelUf);
        }       
    }
}
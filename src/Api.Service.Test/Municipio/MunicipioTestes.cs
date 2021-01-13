using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Dtos.Uf;

namespace src.Api.Service.Test.Municipio
{
    public class MunicipioTestes
    {
        public Guid IdMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string NomeMunicipioAlterado { get; set; }
        public int CodIBGEMunicipio { get; set; }
        public int CodIBGEMunicipioAlterado { get; set; }      
        public Guid UfIdMunicipio { get; set; }
        public Guid UfIdMunicipioAlterado { get; set; }

        public List<MunicipioDto> listMunicipio = new List<MunicipioDto>();
        public MunicipioDto municipioDto;
        public MunicipioDtoCompleto municipioDtoCompleto;
        public MunicipioDtoCreate municipioDtoCreate;
        public MunicipioDtoCreateResult municipioDtoCreateResult;
        public MunicipioDtoUpdate municipioDtoUpdate;
        public MunicipioDtoUpdateResult municipioDtoUpdateResult;

        public MunicipioTestes()
        {
            IdMunicipio = Guid.NewGuid();
            NomeMunicipio = Faker.Address.City();
            CodIBGEMunicipio = Faker.RandomNumber.Next(1000000, 9999999);
            UfIdMunicipio = Guid.NewGuid();
            NomeMunicipioAlterado = Faker.Address.City();
            CodIBGEMunicipioAlterado = Faker.RandomNumber.Next(1000000, 9999999);
            UfIdMunicipioAlterado = Guid.NewGuid();


            for(int i = 0; i < new Random().Next(5, 10); i++)
            {
                listMunicipio.Add(
                    new MunicipioDto()
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                        UfId = Guid.NewGuid()
                    }
                );
            }

            municipioDto = new MunicipioDto()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodIBGEMunicipio,
                UfId = UfIdMunicipio
            };

            municipioDtoCompleto = new MunicipioDtoCompleto()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodIBGEMunicipio,
                UfId = UfIdMunicipio,
                Uf = new UfDto()
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                }
            };

            municipioDtoCreate = new MunicipioDtoCreate()
            {
                Nome = NomeMunicipio,
                CodIBGE = CodIBGEMunicipio,
                UfId = UfIdMunicipio,
            };

            municipioDtoCreateResult = new MunicipioDtoCreateResult()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodIBGEMunicipio,
                UfId = UfIdMunicipio,
                CreateAt = DateTime.UtcNow
            };

            municipioDtoUpdate = new MunicipioDtoUpdate()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipioAlterado,
                CodIBGE = CodIBGEMunicipioAlterado,
                UfId = UfIdMunicipioAlterado,
            };

            municipioDtoUpdateResult = new MunicipioDtoUpdateResult()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipioAlterado,
                CodIBGE = CodIBGEMunicipioAlterado,
                UfId = UfIdMunicipioAlterado,
                UpdateAt = DateTime.UtcNow
            };           
        }        
    }
}
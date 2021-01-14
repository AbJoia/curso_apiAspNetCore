using System;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Dtos.Uf;

namespace src.Api.Service.Test.Cep
{
    public class CepTestes    
    {
        public Guid IdCep { get; set; }
        public string CepCep { get; set; }             
        public string LogradouroCep { get; set; }              
        public string NumeroCep { get; set; }             
        public Guid MunicipioIdCep { get; set; }

        public string CepAlterado { get; set; }
        public string LogradouroAlterado { get; set; }
        public string NumeroAlterado { get; set; } 
        public Guid MunicipioIdAlterado { get; set; }

        public CepDto cepDto;
        public CepDtoCreate dtoCreate;
        public CepDtoCreateResult dtoCreateResult;
        public CepDtoUpdate dtoUpdate;
        public CepDtoUpdateResult dtoUpdateResult;

        public CepTestes()
        {
            IdCep = Guid.NewGuid();
            CepCep = Faker.Address.ZipCode();            
            LogradouroCep = Faker.Address.StreetName();            
            NumeroCep = Faker.RandomNumber.Next(0001, 9999).ToString();            
            MunicipioIdCep = Guid.NewGuid();

            CepAlterado = Faker.Address.ZipCode();
            LogradouroAlterado = Faker.Address.StreetName();
            NumeroAlterado = Faker.RandomNumber.Next(0001, 9999).ToString();
            MunicipioIdAlterado = Guid.NewGuid();

            cepDto = new CepDto()
            {
                Id = IdCep,
                Logradouro = LogradouroCep,
                Cep = CepCep,
                MunicipioId = MunicipioIdCep,
                Numero = NumeroCep,
                Municipio = new MunicipioDtoCompleto
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDto
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                }
            };

            dtoCreate = new CepDtoCreate()
            {
                Cep = CepCep,
                Logradouro = LogradouroCep,
                Numero = NumeroCep,
                MunicipioId = MunicipioIdCep
            };

            dtoCreateResult = new CepDtoCreateResult()
            {
                Id = IdCep,
                Cep = CepCep,
                Logradouro = LogradouroCep,
                Numero = NumeroCep,
                MunicipioId = MunicipioIdCep,
                CreateAt = DateTime.UtcNow,                
            };

            dtoUpdate = new CepDtoUpdate()
            {
                Id = IdCep,
                Cep = CepAlterado,
                Logradouro = LogradouroAlterado,
                Numero = NumeroAlterado,
                MunicipioId = MunicipioIdAlterado,
            };

            dtoUpdateResult = new CepDtoUpdateResult()
            {
                Id = IdCep,
                Cep = CepAlterado,
                Logradouro = LogradouroAlterado,
                Numero = NumeroAlterado,
                MunicipioId = MunicipioIdAlterado,
                UpdateAt = DateTime.UtcNow,
            };
        }
    }
}
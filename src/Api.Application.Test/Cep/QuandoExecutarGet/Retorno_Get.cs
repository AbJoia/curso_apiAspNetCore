using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Application.Test.Cep.QuandoExecutarGet
{
    public class Retorno_Get
    {
       private CepsController _controller;
        
        [Fact(DisplayName = "É Possivel Executar Get")]
        public async Task E_Possivel_Invocar_a_Controller_Cep_Get()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<Guid>()))
                       .ReturnsAsync(
                           new CepDto()
                           {
                               Id = Guid.NewGuid(),
                               Logradouro = Faker.Address.StreetName(),
                               Cep = Faker.Address.ZipCode(),
                               Numero = Faker.RandomNumber.Next(1000, 9999).ToString(),
                               MunicipioId = Guid.NewGuid(),
                               Municipio = new MunicipioDtoCompleto()
                               {
                                    Id = Guid.NewGuid(),
                                    Nome = Faker.Name.FullName(),
                                    CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                                    UfId = Guid.NewGuid(),
                                    Uf = new UfDto()
                                    {
                                        Id = Guid.NewGuid(),
                                        Nome = Faker.Address.UsState(),
                                        Sigla = Faker.Address.UsState().Substring(1, 3)
                                    }     
                               }

                           }
                       );
            _controller = new CepsController(serviceMock.Object);            

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as CepDto;
            Assert.NotNull(resultValue);
            Assert.NotNull(resultValue.Municipio); 
            Assert.NotNull(resultValue.Municipio.Uf);           
        } 
    }
}
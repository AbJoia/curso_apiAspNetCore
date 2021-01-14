using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Application.Test.Municipio.QuandoExecutarGetCompletoByIBGE
{
    public class Retorno_Get
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Executar GetCompletoByIBGE")]
        public async Task E_Possivel_Invocar_a_Controller_GetCompletoByIBGE()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetCompletoByIBGE(It.IsAny<int>()))
                       .ReturnsAsync(
                           new MunicipioDtoCompleto()
                           {
                               Id = Guid.NewGuid(),
                               Nome = Faker.Address.City(),
                               UfId = Guid.NewGuid(),
                               CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                               Uf = new UfDto()
                               {
                                   Id = Guid.NewGuid(),
                                   Nome = Faker.Address.UsState(),
                                   Sigla = Faker.Address.UsState().Substring(1, 3)
                               }
                           }
                       );
                       
            _controller = new MunicipiosController(serviceMock.Object);            

            var result = await _controller.GetCompletoByIBGE(new Random().Next(0000001, 9999999));
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as MunicipioDtoCompleto;
            Assert.NotNull(resultValue);
            Assert.NotNull(resultValue.Uf);
        }
    }
}
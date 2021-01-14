using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Application.Test.Municipio.QuandoExecutarGet
{
    public class Retorno_Get
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Executar Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Get(It.IsAny<Guid>()))
                       .ReturnsAsync(
                           new MunicipioDto()
                           {
                               Id = Guid.NewGuid(),
                               Nome = Faker.Address.City(),
                               UfId = Guid.NewGuid(),
                               CodIBGE = Faker.RandomNumber.Next(0000001, 9999999)
                           }
                       );
                       
            _controller = new MunicipiosController(serviceMock.Object);            

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
        }
    }
}
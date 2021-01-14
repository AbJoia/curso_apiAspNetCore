using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Application.Test.Municipio.QuandoExecutarGetAll
{
    public class Retorno_GetAll
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Executar GetAll")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetAll())
                       .ReturnsAsync( new List<MunicipioDto>(){
                           new MunicipioDto()
                           {
                               Id = Guid.NewGuid(),
                               Nome = Faker.Address.City(),
                               UfId = Guid.NewGuid(),
                               CodIBGE = Faker.RandomNumber.Next(0000001, 9999999)
                           },
                           new MunicipioDto()
                           {
                               Id = Guid.NewGuid(),
                               Nome = Faker.Address.City(),
                               UfId = Guid.NewGuid(),
                               CodIBGE = Faker.RandomNumber.Next(0000001, 9999999)
                           }
                        });
                       
            _controller = new MunicipiosController(serviceMock.Object);                      

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);            
        }
    }
}
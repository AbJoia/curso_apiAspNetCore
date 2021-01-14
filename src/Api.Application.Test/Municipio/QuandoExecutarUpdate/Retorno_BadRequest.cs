using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Application.Test.Municipio.QuandoExecutarUpdate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar Update")]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            var dtoUpdate = new MunicipioDtoUpdate()
                { 
                    Id = Guid.NewGuid(),                   
                    Nome = Faker.Address.City(),
                    UfId = Guid.NewGuid(),
                    CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),                        
                }; 

            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>()))
                       .ReturnsAsync(
                           new MunicipioDtoUpdateResult(){
                               Id = Guid.NewGuid(),
                               Nome = dtoUpdate.Nome,
                               UfId = dtoUpdate.UfId,
                               CodIBGE = dtoUpdate.CodIBGE,
                               UpdateAt = DateTime.UtcNow
                           });
            
            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");            

            var result = await _controller.Put(dtoUpdate);
            Assert.True(result is BadRequestObjectResult); 

            serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>()))
                       .Returns(Task.FromResult<MunicipioDtoUpdateResult>(null));

            _controller = new MunicipiosController(serviceMock.Object);           
            result = await _controller.Put(dtoUpdate);
            Assert.True(result is BadRequestResult);
        }
    }
}
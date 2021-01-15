using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Application.Test.Cep.QuandoExecutarUpdate
{
    public class Retorno_BadRequest
    {
        private CepsController _controller;

        [Fact(DisplayName = "É Possivel Executar Update")]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            var dtoUpdate = new CepDtoUpdate()
            {
                Id = Guid.NewGuid(),
                Cep = "01000-010",
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(000, 999).ToString(),
                MunicipioId = Guid.NewGuid(),
            };

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Put(dtoUpdate))
                       .ReturnsAsync(
                           new CepDtoUpdateResult()
                           {
                                Id = Guid.NewGuid(),
                                UpdateAt = DateTime.UtcNow,
                                Cep = dtoUpdate.Cep,
                                Logradouro = dtoUpdate.Logradouro,
                                Numero = dtoUpdate.Numero,
                                MunicipioId = dtoUpdate.MunicipioId,
                           }); 

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");          

            var result = await _controller.Put(dtoUpdate);
            Assert.True(result is BadRequestObjectResult);

            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Put(dtoUpdate))
                       .Returns(Task.FromResult<CepDtoUpdateResult>(null));
            _controller = new CepsController(serviceMock.Object);

            result = await _controller.Put(dtoUpdate);
            Assert.True(result is BadRequestResult);
                     
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Uf;
using Xunit;

namespace src.Api.Application.Test.Uf.QuandoRequisitarGet
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;
              

        [Fact(DisplayName = "É_Possivel_Invocar_Cotroller_Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IUfService>();
            serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                       .ReturnsAsync(
                           new UfDto()
                           {
                               Id = Guid.NewGuid(),
                               Nome = Faker.Address.UsState(),
                               Sigla = Faker.Address.UsState().Substring(1, 3)
                           }
                       );            
            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("id", "Formato Inválido");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
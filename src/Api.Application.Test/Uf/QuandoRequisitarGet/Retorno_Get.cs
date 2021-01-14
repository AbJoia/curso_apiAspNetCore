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
    public class Retorno_Get    
    {
        private UfsController _controller;

        [Fact(DisplayName = "É Possivel Invocar a Controller Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var ufDto = new UfDto()
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                };

            var serviceMock = new Mock<IUfService>();
            serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                       .ReturnsAsync(ufDto);

            _controller = new UfsController(serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UfDto;
            Assert.Equal(resultValue.Id, ufDto.Id);
            Assert.Equal(resultValue.Nome, ufDto.Nome);
            Assert.Equal(resultValue.Sigla, ufDto.Sigla);
        }
    }
}
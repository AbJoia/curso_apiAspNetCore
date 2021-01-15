using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Application.Test.Cep.QuandoExecutarCreate
{
    public class Retorno_Created
    {
        private CepsController _controller;

        [Fact(DisplayName = "É Possivel Executar Create")]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            var dtoCreate = new CepDtoCreate()
            {
                Cep = "01000-010",
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(000, 999).ToString(),
                MunicipioId = Guid.NewGuid(),
            };

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Post(dtoCreate))
                       .ReturnsAsync(
                           new CepDtoCreateResult()
                           {
                                Id = Guid.NewGuid(),
                                CreateAt = DateTime.UtcNow,
                                Cep = dtoCreate.Cep,
                                Logradouro = dtoCreate.Logradouro,
                                Numero = dtoCreate.Numero,
                                MunicipioId = dtoCreate.MunicipioId,
                           }); 

            _controller = new CepsController(serviceMock.Object);            

            Mock<IUrlHelper> uri = new Mock<IUrlHelper>();
            uri.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
                            .Returns("http://localhost:5000");            
            _controller.Url = uri.Object;

            var result = await _controller.Post(dtoCreate);
            Assert.True(result is CreatedResult);
            var resultValue = ((CreatedResult)result).Value as CepDtoCreateResult;
            Assert.NotNull(resultValue);           
        }
    }
}
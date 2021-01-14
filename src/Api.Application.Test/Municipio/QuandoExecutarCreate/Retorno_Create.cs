using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Application.Test.Municipio.QuandoExecutarCreate
{
    public class Retorno_Create
    {
       private MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar Create")]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            var dtoCreate = new MunicipioDtoCreate()
                {                    
                    Nome = Faker.Address.City(),
                    UfId = Guid.NewGuid(),
                    CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),                        
                }; 

            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Post(It.IsAny<MunicipioDtoCreate>()))
                       .ReturnsAsync(
                           new MunicipioDtoCreateResult(){
                               Id = Guid.NewGuid(),
                               Nome = dtoCreate.Nome,
                               UfId = dtoCreate.UfId,
                               CodIBGE = dtoCreate.CodIBGE,
                               CreateAt = DateTime.UtcNow
                           });
            
            _controller = new MunicipiosController(serviceMock.Object);            

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Post(dtoCreate);
            Assert.True(result is CreatedResult);           

            var resultValue = ((CreatedResult)result).Value as MunicipioDtoCreateResult;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Id != null && resultValue.Id != Guid.Empty);
            Assert.Equal(resultValue.Nome, dtoCreate.Nome);
            Assert.Equal(resultValue.UfId, dtoCreate.UfId);
            Assert.Equal(resultValue.CodIBGE, dtoCreate.CodIBGE);
        }
    }
}
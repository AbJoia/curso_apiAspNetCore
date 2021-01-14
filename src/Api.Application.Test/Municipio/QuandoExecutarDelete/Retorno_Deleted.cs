using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Application.Test.Municipio.QuandoExecutarDelete
{
    public class Retorno_Deleted
    {
        public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Executar Delete")]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                       .ReturnsAsync(true);
            _controller = new MunicipiosController(serviceMock.Object);            

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
            var resultValue = (bool) ((OkObjectResult)result).Value;
            Assert.True(resultValue);
        }
    }
    }
}
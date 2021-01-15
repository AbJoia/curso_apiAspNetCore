using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Application.Test.Cep.QuandoExecutarDelete
{
    public class Retorno_Deleted
    {
        private CepsController _controller;
        
        [Fact(DisplayName = "É Possivel Executar Delete")]
        public async Task E_Possivel_Invocar_a_Controller_Cep()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                       .ReturnsAsync(true);
            _controller = new CepsController(serviceMock.Object);           

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult); 

            var resultValue = (bool) ((OkObjectResult)result).Value;
            Assert.True(resultValue);                       
        }
    }
}
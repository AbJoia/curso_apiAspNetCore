using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Application.Test.Cep.QuandoExecutarGetByCep
{
    public class Retorno_NotFound
    {
        private CepsController _controller;
        
        [Fact(DisplayName = "É Possivel Executar GetByCep")]
        public async Task E_Possivel_Invocar_a_Controller_Cep_GetByCep()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<string>()))
                       .Returns(Task.FromResult<CepDto>(null));
            _controller = new CepsController(serviceMock.Object);            

            var result = await _controller.Get("01000-000");
            Assert.True(result is NotFoundResult);            
        }
    }
}
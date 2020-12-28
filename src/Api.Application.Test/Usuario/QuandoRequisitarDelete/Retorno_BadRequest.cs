using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Delete")]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            var serviceMock = new Mock<IUserService>();           

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");
            
            var result = await _controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);           
        }
    }
}
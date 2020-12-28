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
    public class Retorno_Deleted
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Deleted")]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            var serviceMock = new Mock<IUserService>();           

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object);            

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult); 

            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((Boolean)resultValue);           
        }
    }
}
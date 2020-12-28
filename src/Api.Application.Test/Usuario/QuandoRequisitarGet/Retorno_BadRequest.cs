using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarGet
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Get.")]
        public async Task É_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>()))
                    .ReturnsAsync(
                        new UserDTO
                        {
                            Id = Guid.NewGuid(),
                            Name = nome,
                            Email = email,
                            CreateAt = DateTime.UtcNow
                        }
                    );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);                    
        }
    }
}

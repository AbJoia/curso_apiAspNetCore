using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
    public class Retorno_Updated
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Update")]
        public async Task E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Put(It.IsAny<UserDTOUpdate>()))
                .ReturnsAsync(
                    new UserDTOUpdateResult
                    {
                        Id = Guid.NewGuid(),
                        Name = nome,
                        Email = email,
                        UpdateAt = DateTime.UtcNow
                    }
                );

            _controller = new UsersController(serviceMock.Object);

            var userDTOUpdate = new UserDTOUpdate
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email,
            };

            var result = await _controller.Put(userDTOUpdate);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDTOUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDTOUpdate.Name, resultValue.Name);
            Assert.Equal(userDTOUpdate.Email, resultValue.Email);

            
        }
    }
}
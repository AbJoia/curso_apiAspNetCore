using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarGetAll
{
    public class RetornoGetAll
    {
        private UsersController _controller;

        [Fact(DisplayName = "É POssivel Realizar GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.GetAll())
                .ReturnsAsync(new List<UserDTO>{
                    new UserDTO
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    },
                    new UserDTO
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    }
            }); 

            _controller = new UsersController(serviceMock.Object);
            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value as IEnumerable<UserDTO>;
            Assert.True(resultValue.Count() == 2);        
        }
    }
}
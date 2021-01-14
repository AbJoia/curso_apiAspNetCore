using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Uf;
using Xunit;

namespace src.Api.Application.Test.Uf.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;

       [Fact(DisplayName = "É Possivel Invocar a Controller GetAll")]
       public async Task É_Possivel_Executar_a_Controller_GetAll()
       {           
           var serviceMock = new Mock<IUfService>();
           serviceMock.Setup(m => m.GetAllAsync())
                      .ReturnsAsync(GetListMock(new Random().Next(2, 4)));

            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Comando Inválido");
            
            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);            
       }

        private IEnumerable<UfDto> GetListMock(int qtd)
        {
            List<UfDto> ufs = new List<UfDto>();
            for(int i = 0; i < qtd; i++)
            {
                ufs.Add(
                    new UfDto()
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)   
                    }    
                );
            }
            return ufs;
        }
    }
}
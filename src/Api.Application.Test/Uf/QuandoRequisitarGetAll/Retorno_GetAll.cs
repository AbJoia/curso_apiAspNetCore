using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Uf;
using Xunit;

namespace src.Api.Application.Test.Uf.QuandoRequisitarGetAll
{
    public class Retorno_GetAll
    {
       private UfsController _controller;

       [Fact(DisplayName = "É Possivel Invocar a Controller GetAll")]
       public async Task É_Possivel_Executar_a_Controller_GetAll()
       {
           var listUfDtoMock = GetListMock(new Random().Next(5, 10));

           var serviceMock = new Mock<IUfService>();
           serviceMock.Setup(m => m.GetAllAsync())
                      .ReturnsAsync(listUfDtoMock);

            _controller = new UfsController(serviceMock.Object);
            
            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as List<UfDto>;
            Assert.True(resultValue.Count() == listUfDtoMock.Count());
            Assert.NotNull(resultValue);
            for(int i = 0; i < listUfDtoMock.Count(); i++)
            {
                Assert.Equal(resultValue[i].Id, listUfDtoMock.ToList()[i].Id);
                Assert.Equal(resultValue[i].Nome, listUfDtoMock.ToList()[i].Nome);
                Assert.Equal(resultValue[i].Sigla, listUfDtoMock.ToList()[i].Sigla);
            }

           List<UfDto> emptyList = new List<UfDto>(); 
           serviceMock = new Mock<IUfService>();
           serviceMock.Setup(m => m.GetAllAsync())
                      .ReturnsAsync(emptyList);

            _controller = new UfsController(serviceMock.Object);

            result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            resultValue = ((OkObjectResult)result).Value as List<UfDto>;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Count() == 0);
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
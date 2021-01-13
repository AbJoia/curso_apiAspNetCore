using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Uf;
using Xunit;

namespace src.Api.Service.Test.Uf
{
    public class QuandoExecutarGetAll : UfTestes
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar GetAll")]
        public async Task E_Possivel_Executar_GetAll()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetAllAsync())
                        .ReturnsAsync(listUfDto);
            _service = _serviceMock.Object;

            var result = await _service.GetAllAsync();
            Assert.NotNull(result); 
            Assert.True(result.Count() == listUfDto.Count());
            for(int i = 0; i < listUfDto.Count(); i++)
            {
                Assert.Equal(result.ToList()[i].Id, listUfDto.ToList()[i].Id);
                Assert.Equal(result.ToList()[i].Nome, listUfDto.ToList()[i].Nome);
                Assert.Equal(result.ToList()[i].Sigla, listUfDto.ToList()[i].Sigla);
            }  

            var listEmpty = new List<UfDto>();

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetAllAsync())
                        .ReturnsAsync(listEmpty.AsEnumerable);
            _service = _serviceMock.Object; 

            var emptyResult = await _service.GetAllAsync();
            Assert.NotNull(emptyResult);
            Assert.True(emptyResult.Count() == 0);

        }
    }
}
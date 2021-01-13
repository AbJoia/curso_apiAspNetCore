using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Uf;
using Xunit;

namespace src.Api.Service.Test.Uf
{
    public class QuandoExecutarGet : UfTestes
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;        

        [Fact(DisplayName = "É Possivel Executar Get")]
        public async Task E_Possivel_Executar_Get()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetAsync(IdUf))
                        .ReturnsAsync(ufDto);
            _service = _serviceMock.Object;
            
            var result = await _service.GetAsync(IdUf);
            Assert.NotNull(result); 
            Assert.Equal(result.Id, IdUf);            
            Assert.Equal(result.Nome, NomeUf);
            Assert.Equal(result.Sigla, SiglaUf); 

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                        .Returns(Task.FromResult<UfDto>(null));
            _service = _serviceMock.Object;

            var nullResult = await _service.GetAsync(Guid.NewGuid());
            Assert.Null(nullResult);       
        }
    }
}
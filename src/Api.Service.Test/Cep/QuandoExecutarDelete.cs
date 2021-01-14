using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Service.Test.Cep
{
    public class QuandoExecutarDelete : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Delete")]
        public async Task E_Possivel_Executar_Delete()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Delete(IdCep))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(IdCep);            
            Assert.True(result);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.Delete(Guid.NewGuid());            
            Assert.False(result);            
        }        
    }
}
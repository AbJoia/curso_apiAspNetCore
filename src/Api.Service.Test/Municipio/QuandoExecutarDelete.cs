using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarDelete : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Delete")]
        public async Task E_Possivel_Executar_Delete()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(IdMunicipio);
            Assert.True(result);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.Delete(IdMunicipio);
            Assert.False(result);
        }
    }
}
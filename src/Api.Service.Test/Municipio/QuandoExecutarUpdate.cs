using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarUpdate : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Update")]
        public async Task E_Possivel_Executar_Update()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Put(municipioDtoUpdate))
                        .ReturnsAsync(municipioDtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.Put(municipioDtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdMunicipio);
            Assert.Equal(result.Nome, NomeMunicipioAlterado);
            Assert.Equal(result.UfId, UfIdMunicipioAlterado);
            Assert.Equal(result.CodIBGE, CodIBGEMunicipioAlterado);
            Assert.True(result.UpdateAt != null);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>()))
                        .Returns(Task.FromResult<MunicipioDtoUpdateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.Put(municipioDtoUpdate);
            Assert.Null(result);
        }        
    }
}
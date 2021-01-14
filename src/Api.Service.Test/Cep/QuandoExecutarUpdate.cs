using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Service.Test.Cep
{
    public class QuandoExecutarUpdate : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Update")]
        public async Task E_Possivel_Executar_Update()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Put(dtoUpdate))
                        .ReturnsAsync(dtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.Put(dtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Logradouro, LogradouroAlterado);
            Assert.Equal(result.Numero, NumeroAlterado);
            Assert.Equal(result.Cep, CepAlterado);
            Assert.Equal(result.MunicipioId, MunicipioIdAlterado);
            Assert.True(result.UpdateAt != null);
            Assert.True(result.UpdateAt.CompareTo(dtoCreateResult.CreateAt) > 0);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Put(It.IsAny<CepDtoUpdate>()))
                        .Returns(Task.FromResult<CepDtoUpdateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.Put(dtoUpdate);
            Assert.Null(result);
        }        
    }
}
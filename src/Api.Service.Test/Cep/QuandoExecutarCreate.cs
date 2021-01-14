using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Service.Test.Cep
{
    public class QuandoExecutarCreate : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Create")]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Post(dtoCreate))
                        .ReturnsAsync(dtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.Post(dtoCreate);
            Assert.NotNull(result);            
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Logradouro, LogradouroCep);
            Assert.Equal(result.Cep, CepCep);
            Assert.Equal(result.MunicipioId, MunicipioIdCep);
            Assert.Equal(result.Numero, NumeroCep);
            Assert.True(result.CreateAt != null);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Post(It.IsAny<CepDtoCreate>()))
                        .Returns(Task.FromResult<CepDtoCreateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.Post(dtoCreate);
            Assert.Null(result);            
        }        
    }
}
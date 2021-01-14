using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Interfaces.Services.Cep;
using Xunit;

namespace src.Api.Service.Test.Cep
{
    public class QuandoExecutarGet : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Get")]
        public async Task E_Possivel_Executar_Get()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(IdCep))
                        .ReturnsAsync(cepDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdCep);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Logradouro, LogradouroCep);
            Assert.Equal(result.MunicipioId, MunicipioIdCep);
            Assert.Equal(result.Cep, CepCep);
            Assert.Equal(result.Numero, NumeroCep);
            Assert.NotNull(result.Municipio);
            Assert.NotNull(result.Municipio.Uf);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult<CepDto>(null));
            _service = _serviceMock.Object;

            result = await _service.Get(Guid.NewGuid());
            Assert.Null(result);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(CepCep))
                        .ReturnsAsync(cepDto);
            _service = _serviceMock.Object;

            result = await _service.Get(CepCep);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Logradouro, LogradouroCep);
            Assert.Equal(result.MunicipioId, MunicipioIdCep);
            Assert.Equal(result.Cep, CepCep);
            Assert.Equal(result.Numero, NumeroCep);
            Assert.NotNull(result.Municipio);
            Assert.NotNull(result.Municipio.Uf);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<string>()))
                        .Returns(Task.FromResult<CepDto>(null));
            _service = _serviceMock.Object;

            result = await _service.Get(Guid.NewGuid());
            Assert.Null(result);
        }        
    }
}
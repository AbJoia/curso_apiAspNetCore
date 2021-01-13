using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarGet : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Get")]
        public async Task E_Possivel_Executar_Get()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(IdMunicipio))
                        .ReturnsAsync(municipioDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdMunicipio);
            Assert.NotNull(result);
            Assert.Equal(result.Id, municipioDto.Id);
            Assert.Equal(result.Nome, municipioDto.Nome);
            Assert.Equal(result.UfId, municipioDto.UfId);
            Assert.Equal(result.CodIBGE, municipioDto.CodIBGE);

            result = await _service.Get(Guid.NewGuid());
            Assert.Null(result);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult<MunicipioDto>(null));
            _service = _serviceMock.Object;

            result = await _service.Get(IdMunicipio);
            Assert.Null(result);            
        }        
    }
}
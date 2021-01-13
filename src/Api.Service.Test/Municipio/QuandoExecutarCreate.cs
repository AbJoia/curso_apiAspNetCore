using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarCreate : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Create")]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Post(municipioDtoCreate))
                        .ReturnsAsync(municipioDtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.Post(municipioDtoCreate);
            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Empty && result.Id != null);
            Assert.Equal(result.Nome, NomeMunicipio);
            Assert.Equal(result.CodIBGE, CodIBGEMunicipio);
            Assert.Equal(result.UfId, UfIdMunicipio);
            Assert.True(result.CreateAt != null);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Post(It.IsAny<MunicipioDtoCreate>()))
                        .Returns(Task.FromResult<MunicipioDtoCreateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.Post(municipioDtoCreate);
            Assert.Null(result);
        }        
    }
}
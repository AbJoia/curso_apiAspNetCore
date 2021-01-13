using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarGetByIBGE : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar GetByIBGE")]
        public async Task E_Possivel_Executar_GetByIBGE()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompletoByIBGE(CodIBGEMunicipio))
                        .ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var result = await _service.GetCompletoByIBGE(CodIBGEMunicipio);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdMunicipio);
            Assert.Equal(result.Nome, NomeMunicipio);
            Assert.Equal(result.UfId, UfIdMunicipio);
            Assert.Equal(result.CodIBGE, CodIBGEMunicipio);
            Assert.NotNull(result.Uf);            

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompletoByIBGE(It.IsAny<int>()))
                        .Returns(Task.FromResult<MunicipioDtoCompleto>(null));
            _service = _serviceMock.Object;

            result = await _service.GetCompletoByIBGE(CodIBGEMunicipio);
            Assert.Null(result);            
        }        
    }
}

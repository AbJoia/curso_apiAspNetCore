using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarGetById : MunicipioTestes
    {     
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar GetById")]
        public async Task E_Possivel_Executar_GetById()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompletoById(IdMunicipio))
                        .ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var result = await _service.GetCompletoById(IdMunicipio);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdMunicipio);
            Assert.Equal(result.Nome, NomeMunicipio);
            Assert.Equal(result.UfId, UfIdMunicipio);
            Assert.Equal(result.CodIBGE, CodIBGEMunicipio);
            Assert.NotNull(result.Uf);                       

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompletoById(It.IsAny<Guid>()))
                        .Returns(Task.FromResult<MunicipioDtoCompleto>(null));
            _service = _serviceMock.Object;

            result = await _service.GetCompletoById(IdMunicipio);
            Assert.Null(result);            
        }        
    }
}
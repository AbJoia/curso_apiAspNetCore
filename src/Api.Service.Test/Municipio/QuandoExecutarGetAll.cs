using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;
using Xunit;

namespace src.Api.Service.Test.Municipio
{
    public class QuandoExecutarGetAll : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar GetAll")]
        public async Task E_Possivel_Executar_GetAll()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetAll())
                        .ReturnsAsync(listMunicipio);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == listMunicipio.Count());
            for(int i = 0; i < listMunicipio.Count(); i++)
            {
                Assert.Equal(result.ToList()[i].Id, listMunicipio.ToList()[i].Id);
                Assert.Equal(result.ToList()[i].Nome, listMunicipio.ToList()[i].Nome);
                Assert.Equal(result.ToList()[i].UfId, listMunicipio.ToList()[i].UfId);
                Assert.Equal(result.ToList()[i].CodIBGE, listMunicipio.ToList()[i].CodIBGE);
            }            

            var emptyList = new List<MunicipioDto>();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetAll())
                        .ReturnsAsync(emptyList.AsEnumerable);
            _service = _serviceMock.Object;

            result = await _service.GetAll();
            Assert.Empty(result);
            Assert.True(result.Count() == 0);            
        } 
    }
}
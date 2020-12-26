using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoForExecutadoDelete : UsuarioTestes
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar Delete")]
        public async Task Quando_Executa_Delete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(d => d.Delete(IdUsuario))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(IdUsuario);                       
            Assert.True(result);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(d => d.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            var resultIdInvalido = await _service.Delete(Guid.NewGuid());            
            Assert.False(resultIdInvalido);            
        }        
    }
}
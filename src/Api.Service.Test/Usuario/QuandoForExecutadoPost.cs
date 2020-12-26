using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoForExecutadoPost : UsuarioTestes
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar o Metodo Post")]
        public async Task Quando_For_Executado_Post()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userDTOCreate))
                        .ReturnsAsync(userDTOCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.Post(userDTOCreate);
            Assert.NotNull(result);
            Assert.Equal(userDTOCreate.Email, result.Email);
            Assert.Equal(userDTOCreate.Name, result.Name);
        }
    }
}
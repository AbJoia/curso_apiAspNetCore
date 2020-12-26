using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoForExecutadoGetAll : UsuarioTestes
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar o Metodo GetAll")]
        public async Task Quando_For_Executado_Metodo_GetAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listaUserDTO);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);

            List<UserDTO> emptyList = new List<UserDTO>();
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(emptyList);
            _service = _serviceMock.Object;

            var resulEmpty = await _service.GetAll();
            Assert.Empty(resulEmpty);
            Assert.True(resulEmpty.Count() == 0);
        }
    }
}
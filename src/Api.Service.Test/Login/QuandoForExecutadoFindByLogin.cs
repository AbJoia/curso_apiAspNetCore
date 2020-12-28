using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos;
using src.Api.Domain.Interfaces.Services.User;
using Xunit;

namespace src.Api.Service.Test.Login
{
    public class QuandoForExecutadoFindByLogin
    {
        private ILoginService _service;
        private Mock<ILoginService> _mockService;

        [Fact(DisplayName = "É Possivel Executar Metodo FindByLogin")]
        public async Task E_Possivel_Executar_Metodo_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var returnFindByLogin = new 
            {
                authenticated = true,
                created = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                acessToken = Guid.NewGuid(),
                userName = email,
                message = "Usuário logado com sucesso."
            };

            var loginDto = new LoginDTO
            {
                Email = email,    
            };

            _mockService = new Mock<ILoginService>();
            _mockService.Setup(m => m.FindByLogin(loginDto))
                        .ReturnsAsync(returnFindByLogin);
            _service = _mockService.Object;
            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);
        }
    }
}
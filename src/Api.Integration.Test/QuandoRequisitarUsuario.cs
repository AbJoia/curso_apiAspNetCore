using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.User;
using Xunit;

namespace src.Api.Integration.Test
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email {get; set;}

        [Fact]
        public async Task E_Possviel_Realizar_Crud_Usuario()
        {
            await AdicionarToken();
            _name = Faker.Name.FullName();
            _email = Faker.Internet.Email();

            var userDTO = new UserDTOCreate()
            {
                Name = _name,
                Email = _email
            };

            var response = await PostJsonAsync(userDTO, $"{hostApi}users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDTOCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(userDTO.Name, registroPost.Name);
            Assert.Equal(userDTO.Email, registroPost.Email);
            Assert.True(registroPost.Id != default(Guid));
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

            //Post
            var response = await PostJsonAsync(userDTO, $"{hostApi}users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDTOCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(userDTO.Name, registroPost.Name);
            Assert.Equal(userDTO.Email, registroPost.Email);
            Assert.True(registroPost.Id != default(Guid));

            //GetAll
            response = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Count() > 0);
            Assert.True(listFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);

            //Put
            var updateDtoUser =  new UserDTOUpdate()
            {
                Id = registroPost.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateDtoUser),
                                                  Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<UserDTOUpdateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroPost.Name, registroAtualizado.Name);
            Assert.NotEqual(registroPost.Email, registroAtualizado.Email);  

            //GetId
            response = await client.GetAsync($"{hostApi}users/{registroPost.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDTO>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroAtualizado.Name, registroSelecionado.Name);
            Assert.Equal(registroAtualizado.Email, registroSelecionado.Email);
            Assert.Equal(registroPost.Id, registroSelecionado.Id); 

            //Delete
            response = await client.DeleteAsync($"{hostApi}users/{registroPost.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Get Id apos Delete 
            response = await client.GetAsync($"{hostApi}users/{registroPost.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);      
        }
    }
}
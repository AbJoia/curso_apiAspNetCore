using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Dtos.Municipio;
using Xunit;

namespace src.Api.Integration.Test.Cep
{
    public class QuandoRequisitarCep : BaseIntegration
    {
        [Fact(DisplayName = "É Possivel Executar Crud Cep")]
        public async Task E_Possivel_Executar_Crud_Cep()
        {
            await AdicionarToken();

            //Mock Municipio
            var municipio = new MunicipioDtoCreate()
            {
                Nome = "Petrópolis",
                CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
            };           
            var response = await PostJsonAsync(municipio, $"{hostApi}municipios", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var objMunicipio = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(jsonResult);
            Assert.NotNull(objMunicipio);

            var municipio2 = new MunicipioDtoCreate()
            {
                Nome = "Teresópolis",
                CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
            };           
            response = await PostJsonAsync(municipio2, $"{hostApi}municipios", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var objMunicipio2 = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(jsonResult);
            Assert.NotNull(objMunicipio2);

            //Post
            var dtoCreate = new CepDtoCreate()
            {
                Cep = "01000-000",
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(0001, 9999).ToString(),
                MunicipioId = objMunicipio.Id,
            };

            response = await PostJsonAsync(dtoCreate, $"{hostApi}ceps", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<CepDtoCreateResult>(jsonResult);
            Assert.NotNull(postResult);
            Assert.True(postResult.Id != null && postResult.Id != Guid.Empty);
            Assert.True(postResult.CreateAt != null);
            Assert.Equal(postResult.Logradouro, dtoCreate.Logradouro); 
            Assert.Equal(postResult.MunicipioId, dtoCreate.MunicipioId);
            Assert.Equal(postResult.Numero, dtoCreate.Numero);
            Assert.Equal(postResult.Cep, dtoCreate.Cep);

            //Put
            var dtoUpdate = new CepDtoUpdate()
            {
                Id = postResult.Id,
                Cep = "27666-660",
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(0001, 9999).ToString(),
                MunicipioId = objMunicipio2.Id 
            };

            var content = new StringContent(JsonConvert.SerializeObject(dtoUpdate), 
                                            Encoding.UTF8, 
                                            "Application/json");

            response = await client.PutAsync($"{hostApi}ceps", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var putResult = JsonConvert.DeserializeObject<CepDtoUpdateResult>(jsonResult);
            Assert.NotNull(putResult);
            Assert.Equal(putResult.Id, postResult.Id);
            Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);
            Assert.NotEqual(putResult.Logradouro, postResult.Logradouro); 
            Assert.NotEqual(putResult.MunicipioId, postResult.MunicipioId);
            Assert.NotEqual(putResult.Numero, postResult.Numero);
            Assert.NotEqual(putResult.Cep, postResult.Cep);

            //GetByCep
            response = await client.GetAsync($"{hostApi}ceps/byCep/{putResult.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var getByCepResult = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(getByCepResult);
            Assert.NotNull(getByCepResult.Municipio);
            Assert.NotNull(getByCepResult.Municipio.Uf);
            Assert.Equal(getByCepResult.Id, putResult.Id); 
            Assert.Equal(getByCepResult.Cep, putResult.Cep);
            Assert.Equal(getByCepResult.Logradouro, putResult.Logradouro);
            Assert.Equal(getByCepResult.MunicipioId, putResult.MunicipioId);
            Assert.Equal(getByCepResult.Numero, putResult.Numero);
            Assert.Equal(getByCepResult.Municipio.Id, objMunicipio2.Id);
            Assert.Equal(getByCepResult.Municipio.Nome, objMunicipio2.Nome);
            Assert.Equal(getByCepResult.Municipio.CodIBGE, objMunicipio2.CodIBGE);
            Assert.Equal(getByCepResult.Municipio.UfId, objMunicipio2.UfId);
            Assert.Equal(getByCepResult.Municipio.Uf.Id, objMunicipio2.UfId);
            Assert.Equal("Rio de Janeiro", getByCepResult.Municipio.Uf.Nome);
            Assert.Equal("RJ", getByCepResult.Municipio.Uf.Sigla);

            //GetById
            response = await client.GetAsync($"{hostApi}ceps/{putResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var getByIdResult = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(getByIdResult);           
            Assert.Equal(getByIdResult.Id, putResult.Id); 
            Assert.Equal(getByIdResult.Cep, putResult.Cep);
            Assert.Equal(getByIdResult.Logradouro, putResult.Logradouro);
            Assert.Equal(getByIdResult.MunicipioId, putResult.MunicipioId);
            Assert.Equal(getByIdResult.Numero, putResult.Numero);

            //Delete
            response = await client.DeleteAsync($"{hostApi}ceps/{putResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Get Confirmação Delete
            response = await client.GetAsync($"{hostApi}ceps/{putResult.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
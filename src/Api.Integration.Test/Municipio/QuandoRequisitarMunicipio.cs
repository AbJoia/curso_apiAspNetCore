using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Municipio;
using Xunit;

namespace src.Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        [Fact(DisplayName = "É Possivel Executar Crud Municipio")]
        public async Task E_Possivel_Executar_Crud_Municipio()
        {
            await AdicionarToken();

            var dtoCreate = new MunicipioDtoCreate()
            {
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
            };

            //Post
            var response = await PostJsonAsync(dtoCreate, $"{hostApi}municipios", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var jsonResul = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(jsonResul);
            Assert.NotNull(postResult);
            Assert.True(postResult.Id != null && postResult.Id != Guid.Empty);
            Assert.True(postResult.CreateAt != null);
            Assert.Equal(postResult.Nome, dtoCreate.Nome);
            Assert.Equal(postResult.CodIBGE, dtoCreate.CodIBGE);
            Assert.Equal(postResult.UfId, dtoCreate.UfId);

            //Put
            var dtoUpdate = new MunicipioDtoUpdate()
            {
                Id = postResult.Id,
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(0000001, 9999999),
                UfId = new Guid("7cc33300-586e-4be8-9a4d-bd9f01ee9ad8"),
            };

            var content = new StringContent(
                            JsonConvert.SerializeObject(dtoUpdate),
                            Encoding.UTF8,
                            "Application/json");

            response = await client.PutAsync($"{hostApi}municipios", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResul = await response.Content.ReadAsStringAsync();
            var putResult = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResul);
            Assert.NotNull(putResult);
            Assert.Equal(putResult.Id, postResult.Id);
            Assert.NotEqual(putResult.Nome, postResult.Nome);
            Assert.NotEqual(putResult.UfId, postResult.UfId);
            Assert.NotEqual(putResult.CodIBGE, postResult.CodIBGE);
            Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);

            //GetCompletoById
            response = await client.GetAsync($"{hostApi}municipios/complete/{postResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResul = await response.Content.ReadAsStringAsync();
            var getById = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResul);
            Assert.NotNull(getById);
            Assert.NotNull(getById.Uf);
            Assert.Equal(getById.Id, putResult.Id);
            Assert.Equal(getById.Nome, putResult.Nome);
            Assert.Equal(getById.CodIBGE, putResult.CodIBGE);
            Assert.Equal(getById.UfId, putResult.UfId);
            Assert.Equal(getById.Uf.Id, putResult.UfId);
            Assert.Equal("Alagoas", getById.Uf.Nome);
            Assert.Equal("AL", getById.Uf.Sigla);

            //GetCompletoByIBGE
            response = await client.GetAsync($"{hostApi}municipios/byIBGE/{putResult.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResul = await response.Content.ReadAsStringAsync();
            var getByIBGE = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResul);
            Assert.NotNull(getByIBGE);
            Assert.NotNull(getByIBGE.Uf);
            Assert.Equal(getByIBGE.Id, putResult.Id);
            Assert.Equal(getByIBGE.Nome, putResult.Nome);
            Assert.Equal(getByIBGE.CodIBGE, putResult.CodIBGE);
            Assert.Equal(getByIBGE.UfId, putResult.UfId);
            Assert.Equal(getByIBGE.Uf.Id, putResult.UfId);
            Assert.Equal("Alagoas", getByIBGE.Uf.Nome);
            Assert.Equal("AL", getByIBGE.Uf.Sigla);

            //GetAll
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResul = await response.Content.ReadAsStringAsync();
            var getAll = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResul);
            Assert.NotNull(getAll);
            Assert.True(getAll.Count() > 0);
            Assert.True(getAll.Where(m => m.Id == postResult.Id).Count() == 1);
            Assert.True(getAll.Where(m => m.Id == postResult.Id)
                                     .FirstOrDefault().Nome == putResult.Nome);
            Assert.True(getAll.Where(m => m.Id == postResult.Id)
                                     .FirstOrDefault().UfId == putResult.UfId);
            Assert.True(getAll.Where(m => m.Id == postResult.Id)
                                     .FirstOrDefault().CodIBGE == putResult.CodIBGE);

            //GetID
            response = await client.GetAsync($"{hostApi}municipios/{postResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResul = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<MunicipioDto>(jsonResul);
            Assert.NotNull(getResult);            
            Assert.Equal(getResult.Id, putResult.Id);
            Assert.Equal(getResult.Nome, putResult.Nome);
            Assert.Equal(getResult.CodIBGE, putResult.CodIBGE);
            Assert.Equal(getResult.UfId, putResult.UfId);

            //Delete
            response = await client.DeleteAsync($"{hostApi}municipios/{postResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResul = await response.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<bool>(jsonResul);
            Assert.True(deleteResult);

            //Get Confirmação Delete
            response = await client.GetAsync($"{hostApi}municipios/{postResult.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }        
    }
}
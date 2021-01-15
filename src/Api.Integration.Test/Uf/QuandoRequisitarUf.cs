using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Uf;
using Xunit;

namespace src.Api.Integration.Test.Uf
{
    public class QuandoRequisitarUf : BaseIntegration
    {
        [Fact(DisplayName = "É Possivel Realizar Crud UF")]
        public async Task E_Possivel_Realizar_Crud_UF()
        {
            await AdicionarToken();

            //GetAll
            var response = await client.GetAsync($"{hostApi}ufs");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);
            Assert.NotNull(result);
            Assert.True(result.Count() == 27);
            Assert.True(result.Where(u => u.Sigla == "RJ").Count() == 1);

            //GetId
            var id = result.Where(u => u.Sigla == "RJ").SingleOrDefault().Id;
            response = await client.GetAsync($"{hostApi}ufs/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<UfDto>(jsonResult);
            Assert.NotNull(resultObj);
            Assert.Equal(resultObj.Id, id);
            Assert.Equal("RJ", resultObj.Sigla);
            Assert.Equal("Rio de Janeiro", resultObj.Nome);
        }
    }
}
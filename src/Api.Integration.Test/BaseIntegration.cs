using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using application;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Api.CrossCutting.Mappings;
using src.Api.Data.Context;
using src.Api.Domain.Dtos;

namespace src.Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
       public MyContext mycontext { get; private set; }
       public HttpClient client { get; private set; }
       public IMapper mapper { get; set; }
       public string hostApi { get; set; }
       public HttpResponseMessage response { get; set; } 

       public BaseIntegration()
       {
           hostApi = "http://localhost:5000/api/";
           var builder = new WebHostBuilder()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>();
            var server = new TestServer(builder);

            mycontext = server.Host.Services
                        .GetService(typeof(MyContext)) as MyContext;
            mycontext.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            client = server.CreateClient();
       }

       public async Task AdicionarToken()
       {
           var loginDto = new LoginDTO()
           {
               Email = "adm@mail.com"
           };

            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                         loginObject.acessToken);
       }

       public static async Task<HttpResponseMessage> PostJsonAsync(
                        object dataClass, string url, HttpClient client)
        {
            return await client.PostAsync(url, 
                    new StringContent(JsonConvert.SerializeObject(dataClass), 
                    System.Text.Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            mycontext.Dispose();
            client.Dispose();
        }
    }

    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>{
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            return config.CreateMapper();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Test;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Implementations;
using src.Api.Domain.Entities;
using Xunit;

namespace src.Api.Data.Test
{
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public CepCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;            
        }

        [Fact(DisplayName = "CRUD de Cep")]
        public async Task E_Possivel_Realizar_Crud_Cep()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {                
                MunicipioImplementation _municipioRepository = new MunicipioImplementation(context);
                var municipio = new MunicipioEntity()
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                var municipioResult = await _municipioRepository.InsertAsync(municipio);
                Assert.NotNull(municipioResult);
                Assert.Equal(municipio.Nome, municipioResult.Nome);
                Assert.Equal(municipio.CodIBGE, municipioResult.CodIBGE); 
                Assert.Equal(municipio.UfId, municipioResult.UfId);
                Assert.True(municipioResult.Id != Guid.Empty && municipioResult.Id != null);
                Assert.True(municipioResult.CreateAt != null); 

                CepImplementation _repository = new CepImplementation(context);
                var entity = new CepEntity()
                {  
                    Cep = Faker.Address.ZipCode(),                  
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 1000).ToString(),                    
                    MunicipioId = municipioResult.Id                    
                };

                //Insert
                var result = await _repository.InsertAsync(entity);
                Assert.NotNull(result);
                Assert.Equal(result.Cep, entity.Cep);
                Assert.Equal(result.Logradouro, entity.Logradouro);                 
                Assert.Equal(result.Numero, entity.Numero); 
                Assert.Equal(result.MunicipioId, entity.MunicipioId);
                Assert.True(result.Id != null && result.Id != Guid.Empty);

                //Update
                entity.Logradouro = Faker.Address.StreetName();
                entity.Numero = Faker.RandomNumber.Next(1, 1000).ToString();
                entity.Cep = Faker.Address.ZipCode();
                entity.Id = result.Id;
                var registroAlterado = await _repository.UpdateAsync(entity);
                Assert.NotNull(registroAlterado);
                Assert.Equal(registroAlterado.Cep, entity.Cep);
                Assert.Equal(registroAlterado.Logradouro, entity.Logradouro);                 
                Assert.Equal(registroAlterado.Numero, entity.Numero); 
                Assert.Equal(registroAlterado.MunicipioId, result.MunicipioId);
                Assert.True(registroAlterado.Id == result.Id);

                //Exist
                var exist = await _repository.ExistAsync(result.Id);
                Assert.True(exist);

                exist = await _repository.ExistAsync(Guid.NewGuid());
                Assert.False(exist);

                //Get
                result = await _repository.SelectAsync(registroAlterado.Id);
                Assert.NotNull(result);
                Assert.Equal(result.Cep, registroAlterado.Cep);
                Assert.Equal(result.Logradouro, registroAlterado.Logradouro);                 
                Assert.Equal(result.Numero, registroAlterado.Numero); 
                Assert.Equal(result.MunicipioId, registroAlterado.MunicipioId);
                Assert.True(result.Id == registroAlterado.Id);                

                //GetByCep
                result = await _repository.SelectAsync(registroAlterado.Cep);
                Assert.NotNull(result);
                Assert.Equal(result.Cep, registroAlterado.Cep);
                Assert.Equal(result.Logradouro, registroAlterado.Logradouro);                 
                Assert.Equal(result.Numero, registroAlterado.Numero); 
                Assert.Equal(result.MunicipioId, registroAlterado.MunicipioId);
                Assert.True(result.Id == registroAlterado.Id);
                Assert.NotNull(result.Municipio); 
                Assert.NotNull(result.Municipio.Uf);
                Assert.Equal(result.Municipio.Nome, municipioResult.Nome);
                Assert.Equal("RJ",result.Municipio.Uf.Sigla); 

                //GetAll
                var todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 0);

                //Delete
                var delete = await _repository.DeleteAsync(registroAlterado.Id);
                Assert.True(delete);

                delete = await _repository.DeleteAsync(Guid.NewGuid());
                Assert.False(delete);

                //GetAll Após Delete
                todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() == 0);
            }
        }
    }
}
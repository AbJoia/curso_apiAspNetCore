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
    public class MunicipioCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public MunicipioCrudCompleto(DbTest dbtest)
        {
            _serviceProvider = dbtest.ServiceProvider;
        }

        [Fact(DisplayName = "Crud Completo Municipio")]
        public async Task E_Possivel_Realizar_Crud_Municipio()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repository = new MunicipioImplementation(context);
                var entity = new MunicipioEntity()
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                //Insert
                var result = await _repository.InsertAsync(entity);
                Assert.NotNull(result);
                Assert.Equal(entity.Nome, result.Nome);
                Assert.Equal(entity.CodIBGE, result.CodIBGE); 
                Assert.Equal(entity.UfId, result.UfId);
                Assert.True(result.Id != Guid.Empty && result.Id != null);
                Assert.True(result.CreateAt != null);                
                
                //Update
                entity.Id = result.Id;
                entity.Nome = Faker.Address.City();
                var updateResult = await _repository.UpdateAsync(entity);
                Assert.NotNull(updateResult);
                Assert.Equal(updateResult.Id, entity.Id);
                Assert.Equal(updateResult.Nome, entity.Nome); 
                Assert.Equal(updateResult.UfId, entity.UfId);
                Assert.Equal(updateResult.CodIBGE, entity.CodIBGE); 
                Assert.True(updateResult.UpdateAt.CompareTo(result.CreateAt) > 0);

                //Exist
                var exist = await _repository.ExistAsync(entity.Id);
                Assert.True(exist);

                exist = await _repository.ExistAsync(Guid.NewGuid());
                Assert.False(exist);

                //Get
                result = await _repository.SelectAsync(entity.Id);
                Assert.NotNull(result);
                Assert.Equal(updateResult.Nome, result.Nome);
                Assert.Equal(updateResult.CodIBGE, result.CodIBGE); 
                Assert.Equal(updateResult.UfId, result.UfId);
                Assert.True(result.Id != Guid.Empty && result.Id != null);
                Assert.Null(result.Uf);

                //Get Completo COD. IBGE
                result = await _repository.GetCompleteByCodIBGE(entity.CodIBGE);
                Assert.NotNull(result);
                Assert.Equal(updateResult.Nome, result.Nome);
                Assert.Equal(updateResult.CodIBGE, result.CodIBGE); 
                Assert.Equal(updateResult.UfId, result.UfId);
                Assert.True(result.Id != Guid.Empty && result.Id != null);
                Assert.NotNull(result.Uf);

                //Get Completo ID
                result = await _repository.GetCompleteById(entity.Id);
                Assert.NotNull(result);
                Assert.Equal(updateResult.Nome, result.Nome);
                Assert.Equal(updateResult.CodIBGE, result.CodIBGE); 
                Assert.Equal(updateResult.UfId, result.UfId);
                Assert.True(result.Id != Guid.Empty && result.Id != null);
                Assert.NotNull(result.Uf);                

                //GetAll
                var todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 0);

                //Delete
                var deleteResult = await _repository.DeleteAsync(entity.Id);
                Assert.True(deleteResult);

                deleteResult = await _repository.DeleteAsync(Guid.NewGuid());
                Assert.False(deleteResult); 
            }            
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Test;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Implementations;
using src.Api.Data.Seeds;
using src.Api.Domain.Entities;
using Xunit;

namespace src.Api.Data.Test
{
    public class UfGets : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public UfGets(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Gets de UF")]
        public async Task E_Possivel_Realizar_Gets_UF()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repository = new UfImplementation(context);
                var entity = new UfEntity()
                {
                    Id = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7"),
                    Sigla = "RJ",
                    Nome = "Rio de Janeiro",
                    CreateAt = DateTime.UtcNow
                };

                //Exist
                var result = await _repository.ExistAsync(entity.Id);
                Assert.True(result);

                //Get
                var selectResult = await _repository.SelectAsync(entity.Id); 
                Assert.NotNull(selectResult);
                Assert.Equal(entity.Id, selectResult.Id);
                Assert.Equal(entity.Sigla, selectResult.Sigla);
                Assert.Equal(entity.Nome, selectResult.Nome);

                //GetAll
                var todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() == 27);
            }            
        }
    }
}
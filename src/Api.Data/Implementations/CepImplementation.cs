using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using src.Api.Domain.Repository;

namespace src.Api.Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataSet;

        public CepImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<CepEntity>();
        }
        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataSet.Include(c => c.Municipio)
                                 .ThenInclude(m => m.Uf)   
                                 .SingleOrDefaultAsync(c => c.Cep.Equals(cep));
        }
    }
}
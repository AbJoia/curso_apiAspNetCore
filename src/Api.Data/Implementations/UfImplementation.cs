using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using src.Api.Domain.Repository;

namespace src.Api.Data.Implementations
{
    public class UfImplementation : BaseRepository<UfEntity>, IUfRepository
    {
        private DbSet<UfEntity> _dataSet;

        public UfImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<UfEntity>();            
        }        
    }
}
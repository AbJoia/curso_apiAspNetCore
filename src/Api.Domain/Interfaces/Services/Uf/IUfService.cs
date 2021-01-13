using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Uf;

namespace src.Api.Domain.Interfaces.Services.Uf
{
    public interface IUfService
    {
        Task<UfDto> GetAsync(Guid id);
        Task<IEnumerable<UfDto>> GetAllAsync();
    }
}
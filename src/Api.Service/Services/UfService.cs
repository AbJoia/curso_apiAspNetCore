using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.Uf;
using src.Api.Domain.Interfaces.Services.Uf;
using src.Api.Domain.Repository;

namespace src.Api.Service.Services
{
    public class UfService : IUfService
    {
        private IUfRepository _repository;
        private IMapper _mapper;

        public UfService(IUfRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;            
        }

        public async Task<IEnumerable<UfDto>> GetAllAsync()
        {
            var result = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UfDto>>(result);
        }

        public async Task<UfDto> GetAsync(Guid id)
        {
            var result = await _repository.SelectAsync(id);
            return _mapper.Map<UfDto>(result);
        }
    }
}
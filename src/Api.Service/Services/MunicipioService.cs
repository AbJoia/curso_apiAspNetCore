using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Services.Municipio;
using src.Api.Domain.Models;
using src.Api.Domain.Repository;

namespace src.Api.Service.Services
{
    public class MunicipioService : IMunicipioService
    {
        private IMunicipioRepository _repository;
        private IMapper _mapper;

        public MunicipioService(IMunicipioRepository repository, IMapper mapper)
        {
           _repository = repository;
           _mapper = mapper; 
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<MunicipioDto> Get(Guid id)
        {
            var result = await _repository.SelectAsync(id);
            return _mapper.Map<MunicipioDto>(result);
            
        }

        public async Task<IEnumerable<MunicipioDto>> GetAll()
        {
            var result = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<MunicipioDto>>(result);
        }

        public async Task<MunicipioDtoCompleto> GetCompletoByIBGE(int codIBGE)
        {
            var result = await _repository.GetCompleteByCodIBGE(codIBGE);
            return _mapper.Map<MunicipioDtoCompleto>(result);
        }

        public async Task<MunicipioDtoCompleto> GetCompletoById(Guid id)
        {
            var result = await _repository.GetCompleteById(id);
            return _mapper.Map<MunicipioDtoCompleto>(result);
        }

        public async Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio)
        {
            var model = _mapper.Map<MunicipioModel>(municipio);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<MunicipioDtoCreateResult>(result);
        }

        public async Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio)
        {
            var model = _mapper.Map<MunicipioModel>(municipio);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<MunicipioDtoUpdateResult>(result);
        }
    }
}
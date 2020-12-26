using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Services.User;
using src.Api.Domain.Models;

namespace src.Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository;

        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository =repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDTO> Get(Guid id)
        {
           var entity = await _repository.SelectAsync(id);
           return _mapper.Map<UserDTO>(entity) ?? new UserDTO();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var listEntity =  await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(listEntity);
        }

        public async Task<UserDTOCreateResult> Post(UserDTOCreate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var userEntity = _mapper.Map<UserEntity>(model);
            var entity = await _repository.InsertAsync(userEntity);
            return _mapper.Map<UserDTOCreateResult>(entity);
        }

        public async Task<UserDTOUpdateResult> Put(UserDTOUpdate user)
        {
           var model = _mapper.Map<UserModel>(user);
           var userEntity = _mapper.Map<UserEntity>(model);
           var entity = await _repository.UpdateAsync(userEntity); 
           return _mapper.Map<UserDTOUpdateResult>(entity);
        }
    }
}
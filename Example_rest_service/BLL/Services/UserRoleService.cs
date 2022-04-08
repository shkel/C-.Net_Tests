using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class  UserRoleService : ITrainingService<UserRole>
    {
        private IRepository<UserRoleDAL> _repository;
        private readonly ILogger<UserRoleService> _logger;
        private readonly IMapper _mapper;

        public UserRoleService(IRepository<UserRoleDAL> repository, IMapper mapper, ILogger<UserRoleService> logger)
        {
            if (repository == null)
            {
                throw new TrainingException("Repository is Null");
            }
            if (logger == null)
            {
                throw new TrainingException("Logger is Null");
            }
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public UserRole Create(UserRole item)
        {
            if (item == null)
            {
                throw new TrainingException("The UserRole is null");
            }
            // TODO : db request ?
            if (item.UserId < 1)
            {
                throw new TrainingException("Invalid UserId");
            }
            // TODO : db request ?
            if (item.RoleId < 1)
            {
                throw new TrainingException("Invalid RoleId");
            }
            try
            {
                return _mapper.Map<UserRole>(_repository.Create(_mapper.Map<UserRoleDAL>(item)));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error creating UserRole");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error creating UserRole. Wrong data.", e);
            }
        }

        public void Update(UserRole item)
        {
            if (item == null)
            {
                throw new TrainingException("The UserRole is null");
            }
            try
            {
                _repository.Update(_mapper.Map<UserRole, UserRoleDAL>(item));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error updating UserRole");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error updating UserRole. Wrong data.", e);
            }

        }

        public void Delete(UserRole item)
        {
            if (item == null)
            {
                throw new TrainingException("The UserRole is null");
            }
            _repository.Delete(_mapper.Map<UserRole, UserRoleDAL>(item));
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return _mapper.Map<List<UserRole>>(await _repository.GetAllAsync());
        }

        public UserRole Get(int id)
        {
            return _mapper.Map<UserRole>(_repository.GetByKey(id));
        }
    }
}

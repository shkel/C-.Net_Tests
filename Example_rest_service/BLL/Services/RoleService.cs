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
    public class RoleService : ITrainingService<Role>
    {
        private IRepository<RoleDAL> _repository;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public RoleService(IRepository<RoleDAL> repository, IMapper mapper, ILogger<RoleService> logger)
        {
            if (repository == null)
            {
                throw new TrainingException("Repositpry is Null");
            }
            if (logger == null)
            {
                throw new TrainingException("Logger is Null");
            }
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Role Create(Role item)
        {
            if (item == null)
            {
                throw new TrainingException("The role is null");
            }
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new TrainingException("The name of role is not specified");
            }
            try
            {
                return _mapper.Map<Role>(_repository.Create(_mapper.Map<RoleDAL>(item)));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error creating role");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error creating role. Wrong data.", e);
            }
        }

        public void Update(Role item)
        {
            if (item == null)
            {
                throw new TrainingException("The role is null");
            }
            try
            {
                _repository.Update(_mapper.Map<RoleDAL>(item));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error updating role");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error updating role. Wrong data.", e);
            }

        }

        public void Delete(Role item)
        {
            if (item == null)
            {
                throw new TrainingException("The role is null");
            }
            _repository.Delete(_mapper.Map<RoleDAL>(item));
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return _mapper.Map<List<Role>>(await _repository.GetAllAsync());
        }

        public Role Get(int id)
        {
            return _mapper.Map<Role>(_repository.GetByKey(id));
        }
    }
}

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
    public class  UserService : ITrainingService<User>
    {
        private IRepository<UserDAL> _repository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserDAL> repository, IMapper mapper, ILogger<UserService> logger)
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

        public User Create(User item)
        {
            if (item == null)
            {
                throw new TrainingException("The User is null");
            }
            if (String.IsNullOrEmpty(item.Email))
            {
                throw new TrainingException("Email is empty");
            }
            // TODO : smart check
            if (!item.Email.Contains("@"))
            {
                throw new TrainingException("Invalid email");
            }
            try
            {
               return _mapper.Map<User>(_repository.Create(_mapper.Map<User, UserDAL>(item)));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error creating User");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error creating User. Wrong data.", e);
            }
        }

        public void Update(User item)
        {
            if (item == null)
            {
                throw new TrainingException("The User is null");
            }
            try
            {
                _repository.Update(_mapper.Map<User, UserDAL>(item));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error updating User");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error updating User. Wrong data.", e);
            }

        }

        public void Delete(User item)
        {
            if (item == null)
            {
                throw new TrainingException("The User is null");
            }
            _repository.Delete(_mapper.Map<User, UserDAL>(item));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return _mapper.Map<List<User>>(await _repository.GetAllAsync());
        }

        public User Get(int id)
        {
            return _mapper.Map<User>(_repository.GetByKey(id));
        }
    }
}

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
    public class  TrainingService : ITrainingService<Training>
    {
        private IRepository<TrainingDAL> _repository;
        private readonly ILogger<TrainingService> _logger;
        private readonly IMapper _mapper;

        public TrainingService(IRepository<TrainingDAL> repository, IMapper mapper, ILogger<TrainingService> logger)
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

        public Training Create(Training item)
        {
            if (item == null)
            {
                throw new TrainingException("The Training is null");
            }
            if (item.LessonId < 1)
            {
                throw new TrainingException("Invalid LessonId");
            }
            // TODO : db request to test role
            if (item.StudentId < 1)
            {
                throw new TrainingException("Invalid StudentId");
            }
            try
            {
                return _mapper.Map<Training>(_repository.Create(_mapper.Map<TrainingDAL>(item)));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error creating Training");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error creating Training. Wrong data.", e);
            }
        }

        public void Update(Training item)
        {
            if (item == null)
            {
                throw new TrainingException("The Training is null");
            }
            try
            {
                _repository.Update(_mapper.Map<Training, TrainingDAL>(item));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error updating Training");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error updating Training. Wrong data.", e);
            }

        }

        public void Delete(Training item)
        {
            if (item == null)
            {
                throw new TrainingException("The Training is null");
            }
            _repository.Delete(_mapper.Map<Training, TrainingDAL>(item));
        }

        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return _mapper.Map<List<Training>>(await _repository.GetAllAsync());
        }

        public Training Get(int id)
        {
            return _mapper.Map<Training>(_repository.GetByKey(id));
        }
    }
}

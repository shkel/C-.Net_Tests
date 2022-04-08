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
    public class  LessonService : ITrainingService<Lesson>
    {
        private IRepository<LessonDAL> _repository;
        private readonly ILogger<LessonService> _logger;
        private readonly IMapper _mapper;

        public LessonService(IRepository<LessonDAL> repository, IMapper mapper, ILogger<LessonService> logger)
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

        public Lesson Create(Lesson item)
        {
            if (item == null)
            {
                throw new TrainingException("The Lesson is null");
            }
            if (item.CourseId < 1)
            {
                throw new TrainingException("Invalid CourseId");
            }
            // TODO : db request ?
            if (item.LectorID < 1)
            {
                throw new TrainingException("Invalid LectorID");
            }
            try
            {
                return _mapper.Map<Lesson>(_repository.Create(_mapper.Map<Lesson, LessonDAL>(item)));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error creating Lesson");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error creating Lesson. Wrong data.", e);
            }
        }

        public void Update(Lesson item)
        {
            if (item == null)
            {
                throw new TrainingException("The Lesson is null");
            }
            try
            {
                _repository.Update(_mapper.Map<Lesson, LessonDAL>(item));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error updating Lesson");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error updating Lesson. Wrong data.", e);
            }

        }

        public void Delete(Lesson item)
        {
            if (item == null)
            {
                throw new TrainingException("The Lesson is null");
            }
            _repository.Delete(_mapper.Map<Lesson, LessonDAL>(item));
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return _mapper.Map<List<Lesson>>(await _repository.GetAllAsync());
        }

        public Lesson Get(int id)
        {
            return _mapper.Map<Lesson>(_repository.GetByKey(id));
        }
    }
}

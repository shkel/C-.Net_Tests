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
    public class  CourseService : ITrainingService<Course>
    {
        private IRepository<CourseDAL> _repository;
        private readonly ILogger<CourseService> _logger;
        private readonly IMapper _mapper;

        public CourseService(IRepository<CourseDAL> repository, IMapper mapper, ILogger<CourseService> logger)
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

        public Course Create(Course item)
        {
            if (item == null)
            {
                throw new TrainingException("The Course is null");
            }
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new TrainingException("The name of Course is not specified");
            }
            try
            {
               return _mapper.Map<CourseDAL, Course>(_repository.Create(_mapper.Map<Course, CourseDAL>(item)));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error creating course");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error creating course. Wrong data.", e);
            }
        }

        public void Update(Course item)
        {
            if (item == null)
            {
                throw new TrainingException("The Course is null");
            }
            try
            {
                _repository.Update(_mapper.Map<Course, CourseDAL>(item));
            }
            catch (DbUpdateException)
            {
                throw new TrainingException("Error updating Course");
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Error updating Course. Wrong data.", e);
            }

        }

        public void Delete(Course item)
        {
            if (item == null)
            {
                throw new TrainingException("The Course is null");
            }
            _repository.Delete(_mapper.Map<Course, CourseDAL>(item));
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return _mapper.Map<List<Course>>(await _repository.GetAllAsync());
        }

        public Course Get(int id)
        {
            return _mapper.Map<CourseDAL, Course>(_repository.GetByKey(id));
        }
    }
}

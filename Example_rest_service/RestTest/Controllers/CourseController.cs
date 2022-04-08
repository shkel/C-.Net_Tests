using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using NLog;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL;

namespace Controllers
{
    #region snippet_CourseController
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ITrainingService<Course> _service;
    #endregion
        private readonly IMapper _mapper;

        private readonly ILogger<CourseController> _logger;

        public CourseController(ITrainingService<Course> service, IMapper mapper, ILogger<CourseController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        #region snippet_GetAll
        [HttpGet]
        public async Task<ActionResult<List<CourseModel>>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<CourseModel>>(await _service.GetAllAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course.GetAllAsync");
                throw new TrainingException("Something wrong with Couse API", e);
            }
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetCourse")]
        public ActionResult<CourseModel> GetById(int id)
        {
            try
            {
                var item = _service.Get(id);
                if (item == null)
                {
                    return NotFound();
                }

                return _mapper.Map<CourseModel>(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course.GetById");
                throw new TrainingException("Something wrong with Couse API", e);
            }
        }
        #endregion

        #region snippet_Create
        /// <summary>
        /// Creates a Course.
        /// </summary>
        /// <param name="course"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CourseModel> Create(CourseModel course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            try
            {
                return _mapper.Map<CourseModel>(_service.Create(_mapper.Map<Course>(course)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course.Create");
                throw new TrainingException("Something wrong with Couse API", e);
            }
        }
        #endregion

        #region snippet_Update
        [HttpPut]
        public IActionResult Update(CourseModel course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            try
            {
                var CourseInDB = _service.Get(course.Id);
                if (CourseInDB == null)
                {
                    return NotFound();
                }

                _service.Update(_mapper.Map<Course>(course));

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course.Update");
                throw new TrainingException("Something wrong with Couse API", e);
            }
        }
        #endregion

        #region snippet_Delete
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var course = _service.Get(id);

                if (course == null)
                {
                    return NotFound();
                }

                _service.Delete(course);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course.Delete");
                throw new TrainingException("Something wrong with Couse API", e);
            }
        }
        #endregion
    }
}
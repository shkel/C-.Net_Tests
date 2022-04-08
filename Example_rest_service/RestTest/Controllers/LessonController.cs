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
    #region snippet_LessonController
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ITrainingService<Lesson> _service;
    #endregion
        private readonly IMapper _mapper;

        private readonly ILogger<LessonController> _logger;

        public LessonController(ITrainingService<Lesson> service, IMapper mapper, ILogger<LessonController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        #region snippet_GetAll
        [HttpGet]
        public async Task<ActionResult<List<LessonModel>>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<LessonModel>>(await _service.GetAllAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lesson.GetAllAsync");
                throw new TrainingException("Something wrong with Lesson API", e);
            }
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetLesson")]
        public ActionResult<LessonModel> GetById(int id)
        {
            try
            {
                var item = _service.Get(id);
                if (item == null)
                {
                    return NotFound();
                }

                return _mapper.Map<LessonModel>(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lesson.GetById");
                throw new TrainingException("Something wrong with Lesson API", e);
            }
        }
        #endregion

        #region snippet_Create
        /// <summary>
        /// Creates a Lesson.
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LessonModel> Create(LessonModel lesson)
        {
            if (lesson == null)
            {
                return BadRequest();
            }
            try
            {
                return _mapper.Map<LessonModel>(_service.Create(_mapper.Map<Lesson>(lesson)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lesson.Create");
                throw new TrainingException("Something wrong with Lesson API", e);
            }
        }
        #endregion

        #region snippet_Update
        [HttpPut]
        public IActionResult Update(LessonModel lesson)
        {
            if (lesson == null)
            {
                return BadRequest();
            }
            try
            {
                var LessonInDB = _service.Get(lesson.Id);
                if (LessonInDB == null)
                {
                    return NotFound();
                }

                _service.Update(_mapper.Map<Lesson>(lesson));

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lesson.Update");
                throw new TrainingException("Something wrong with Lesson API", e);
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
                var lesson = _service.Get(id);

                if (lesson == null)
                {
                    return NotFound();
                }

                _service.Delete(lesson);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lesson.Delete");
                throw new TrainingException("Something wrong with Lesson API", e);
            }
        }
        #endregion
    }
}
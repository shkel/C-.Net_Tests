using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    #region snippet_TrainingController
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService<Training> _service;
    #endregion
        private readonly IMapper _mapper;

        private readonly ILogger<TrainingController> _logger;

        public TrainingController(ITrainingService<Training> service, IMapper mapper, ILogger<TrainingController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        #region snippet_GetAll
        [HttpGet]
        public async Task<ActionResult<List<TrainingModel>>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<TrainingModel>>(await _service.GetAllAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Training.GetAllAsync");
                throw new TrainingException("Something wrong with Training API", e);
            }
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetTraining")]
        public ActionResult<TrainingModel> GetById(int id)
        {
            try
            {
                var item = _service.Get(id);

                if (item == null)
                {
                    return NotFound();
                }

                return _mapper.Map<TrainingModel>(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Training.GetById");
                throw new TrainingException("Something wrong with Training API", e);
            }
        }
        #endregion

        #region snippet_Create
        /// <summary>
        /// Creates a Training.
        /// </summary>
        /// <param name="training"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TrainingModel> Create(TrainingModel training)
        {
            if (training == null)
            {
                return BadRequest();
            }
            try
            {
                return _mapper.Map<TrainingModel>(_service.Create(_mapper.Map<Training>(training)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Training.Create");
                throw new TrainingException("Something wrong with Training API", e);
            }
        }
        #endregion

        #region snippet_Update
        [HttpPut]
        public IActionResult Update(TrainingModel training)
        {
            if (training == null)
            {
                return BadRequest();
            }
            try
            {
                var TrainingInDB = _service.Get(training.Id);
                if (TrainingInDB == null)
                {
                    return NotFound();
                }

                _service.Update(_mapper.Map<Training>(training));

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Training.Update");
                throw new TrainingException("Something wrong with Training API", e);
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
                var training = _service.Get(id);

                if (training == null)
                {
                    return NotFound();
                }

                _service.Delete(training);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Training.Delete");
                throw new TrainingException("Something wrong with Training API", e);
            }
        }
        #endregion
    }
}
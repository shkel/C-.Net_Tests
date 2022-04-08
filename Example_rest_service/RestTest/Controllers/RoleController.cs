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
    #region snippet_RoleController
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ITrainingService<Role> _service;
    #endregion
        private readonly IMapper _mapper;

        private readonly ILogger<RoleController> _logger;

        public RoleController(ITrainingService<Role> service, IMapper mapper, ILogger<RoleController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        #region snippet_GetAll
        [HttpGet]
        public async Task<ActionResult<List<RoleModel>>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<RoleModel>>(await _service.GetAllAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Role.GetAllAsync");
                throw new TrainingException("Something wrong with Role API", e);
            }
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetRole")]
        public ActionResult<RoleModel> GetById(int id)
        {
            try
            {
                var item = _service.Get(id);

                if (item == null)
                {
                    return NotFound();
                }

                return _mapper.Map<RoleModel>(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Role.GetAllAsync");
                throw new TrainingException("Something wrong with Role API", e);
            }
        }
        #endregion

        #region snippet_Create
        /// <summary>
        /// Creates a Role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoleModel> Create(RoleModel role)
        {
            if (role == null)
            {
                return BadRequest();
            }
            try
            {
                return _mapper.Map<RoleModel>(_service.Create(_mapper.Map<Role>(role)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Role.Create");
                throw new TrainingException("Something wrong with Role API", e);
            }
        }
        #endregion

        #region snippet_Update
        [HttpPut]
        public IActionResult Update(RoleModel role)
        {
            if (role == null)
            {
                return BadRequest();
            }
            try
            {
                var RoleInDB = _service.Get(role.Id);
                if (RoleInDB == null)
                {
                    return NotFound();
                }

                _service.Update(_mapper.Map<Role>(role));

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Role.Update");
                throw new TrainingException("Something wrong with Role API", e);
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
                var role = _service.Get(id);

                if (role == null)
                {
                    return NotFound();
                }

                _service.Delete(role);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Role.Delete");
                throw new TrainingException("Something wrong with Role API", e);
            }
        }
        #endregion
    }
}
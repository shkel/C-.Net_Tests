using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    #region snippet_UserRoleController
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ITrainingService<UserRole> _service;
    #endregion
        private readonly IMapper _mapper;

        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(ITrainingService<UserRole> service, IMapper mapper, ILogger<UserRoleController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        #region snippet_GetAll
        [HttpGet]
        public async Task<ActionResult<List<UserRoleModel>>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<UserRoleModel>>(await _service.GetAllAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UserRole.GetAllAsync");
                throw new TrainingException("Something wrong with UserRole API", e);
            }
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetUserRole")]
        public ActionResult<UserRoleModel> GetById(int id)
        {
            try
            {
                var item = _service.Get(id);

                if (item == null)
                {
                    return NotFound();
                }

                return _mapper.Map<UserRoleModel>(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UserRole.GetById");
                throw new TrainingException("Something wrong with UserRole API", e);
            }
        }
        #endregion

        #region snippet_Create
        /// <summary>
        /// Creates a UserRole.
        /// </summary>
        /// <param name="UserRole"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserRoleModel> Create(UserRoleModel userRole)
        {
            if (userRole == null)
            {
                return BadRequest();
            }
            try
            {
                return _mapper.Map<UserRoleModel>(_service.Create(_mapper.Map<UserRole>(userRole)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UserRole.Create");
                throw new TrainingException("Something wrong with UserRole API", e);
            }
        }
        #endregion

        #region snippet_Update
        [HttpPut]
        public IActionResult Update(UserRoleModel userRole)
        {
            if (userRole == null)
            {
                return BadRequest();
            }
            try
            {
                    var UserRoleInDB = _service.Get(userRole.Id);
                if (UserRoleInDB == null)
                {
                    return NotFound();
                }

                _service.Update(_mapper.Map<UserRole>(userRole));

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UserRole.Update");
                throw new TrainingException("Something wrong with UserRole API", e);
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
                var userRole = _service.Get(id);

                if (userRole == null)
                {
                    return NotFound();
                }

                _service.Delete(userRole);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UserRole.Delete");
                throw new TrainingException("Something wrong with UserRole API", e);
            }
        }
        #endregion
    }
}
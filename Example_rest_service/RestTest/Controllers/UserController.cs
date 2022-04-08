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
    #region snippet_UserController
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITrainingService<User> _service;
    #endregion
        private IMapper _mapper;

        private readonly ILogger<UserController> _logger;

        public UserController(ITrainingService<User> service, IMapper mapper, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        #region snippet_GetAll
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<UserModel>>(await _service.GetAllAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User.GetAllAsync");
                throw new TrainingException("Something wrong with User API", e);
            }
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<UserModel> GetById(int id)
        {
            try
            {
                var item = _service.Get(id);

                if (item == null)
                {
                    return NotFound();
                }

                return _mapper.Map<UserModel>(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User.GetById");
                throw new TrainingException("Something wrong with User API", e);
            }
        }
        #endregion

        #region snippet_Create
        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserModel> Create(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            return _mapper.Map<UserModel>(_service.Create(_mapper.Map<User>(user)));
        }
        #endregion

        #region snippet_Update
        [HttpPut]
        public IActionResult Update(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            try
            {
                var userInDB = _service.Get(user.Id);
                if (userInDB == null)
                {
                    return NotFound();
                }

                _service.Update(_mapper.Map<User>(user));

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User.Update");
                throw new TrainingException("Something wrong with User API", e);
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
                var user = _service.Get(id);

                if (user == null)
                {
                    return NotFound();
                }

                _service.Delete(user);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User.Delete");
                throw new TrainingException("Something wrong with User API", e);
            }
        }
        #endregion
    }
}
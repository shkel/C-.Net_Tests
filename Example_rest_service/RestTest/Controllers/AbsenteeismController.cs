using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    #region snippet_AbsenteeismReportController
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenteeismReportController : ControllerBase
    {
        private readonly ReportsService _service;
    #endregion
        private readonly IMapper _mapperBllToWeb;

        private readonly ILogger<AbsenteeismReportController> _logger;
        

        public AbsenteeismReportController(ReportsService service, IMapper mapperBllToWeb, ILogger<AbsenteeismReportController> logger)
        {
            _service = service;
            _logger = logger;
            _mapperBllToWeb = mapperBllToWeb;
        }

        #region snippet_GetAttendanceByCourse
        [HttpGet("reportByStudentByCourse", Name = "GetAbsenteeismAsync")]
        public async Task<ActionResult<List<AbsenteeismReportModel>>> GetAbsenteeismAsync([FromQuery] string studentFirstName, [FromQuery] string studentLastName, [FromQuery] string courseName)
        {
            return _mapperBllToWeb.Map<List<AbsenteeismReportModel>>(await _service.SendAbsenteeismReportAsync(studentFirstName, studentLastName, courseName));
        }
        #endregion

        #region snippet_GetAttendanceByCourse
        [HttpGet("reportAvg", Name = "GetAvgStudentScoreAsync")]
        public async Task<decimal> GetAvgStudentScoreAsync([FromQuery] int studentId, [FromQuery] int courseId)
        {
            return await _service.SendAVGUserScoreAsync(studentId, courseId);
        }
        #endregion
    }
}
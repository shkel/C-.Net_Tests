using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    #region snippet_AttendanceController
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ReportsService _service;
    #endregion
        private readonly IMapper _mapperBllToWeb;

        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(ReportsService service, IMapper mapperBllToWeb, ILogger<AttendanceController> logger)
        {
            _service = service;
            _logger = logger;
            _mapperBllToWeb = mapperBllToWeb;
        }

        #region snippet_GetAttendanceByCourse
        [HttpGet("reportByStudent", Name = "GetAttendanceByStudent")]
        public async Task<ActionResult<List<AttendanceReportModel>>> GetAttendanceByStudent([FromQuery] string studentFirstName, [FromQuery] string studentLastName)
        {
            try
            {
                return _mapperBllToWeb.Map<List<AttendanceReportModel>>(await _service.GetAttendanceAsync(studentFirstName, studentLastName));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAttendanceByStudent");
                throw new TrainingException("Can't get Attendance Report", e);
            }
        }
        #endregion

        #region snippet_GetAttendanceByCourse
        [HttpGet("reportByCourse", Name = "GetAttendanceByCourse")]
        public async Task<ActionResult<List<AttendanceReportModel>>> GetAttendanceByCourse([FromQuery] string courseName, [FromQuery] string lessonTime)
        {

            try
            {
                return _mapperBllToWeb.Map<List<AttendanceReportModel>>(await _service.GetAttendanceAsync(courseName, DateTime.Parse(lessonTime)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAttendanceByCourse");
                throw new TrainingException("Can't get Attendance Report", e);
            }
        }
        #endregion
    }
}
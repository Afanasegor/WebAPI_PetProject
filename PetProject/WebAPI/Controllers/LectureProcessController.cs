using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using BusinessLogic;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureProcessController : ControllerBase
    {
        private readonly ILectureProcessService lectureProcess;

        public LectureProcessController(ILectureProcessService lectureProcessService)
        {
            this.lectureProcess = lectureProcessService;
        }

        [HttpGet("startlecture/{lectureId}")]
        public IEnumerable<Attendance> StartLecture(int lectureId)
        {
            var attendanceList = lectureProcess.StartLecture(lectureId);
            return attendanceList;
        }        

        [HttpGet("attendance/present/{lectureId}/{studentId}/{homeworkMark?}")]
        public Attendance MarkAttendance(int lectureId, int studentId, int homeworkMark = 0)
        {            
            var attendance = lectureProcess.MarkAttendance(lectureId, studentId, homeworkMark);
            return attendance;
        }

        [HttpGet("finishlecture/{lectureId}")]
        public IEnumerable<Attendance> FinishLecture(int lectureId)
        {
            var attendanceList = lectureProcess.FinishLecture(lectureId);
            return attendanceList;
        }
    }
}

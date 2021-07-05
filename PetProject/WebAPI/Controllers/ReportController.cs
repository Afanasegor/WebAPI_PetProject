using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Service;
using DataAccess;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportGeneratorService reportGenerator;
        public ReportController(IReportGeneratorService reportGenerator)
        {
            this.reportGenerator = reportGenerator;
        }


        [HttpGet("report/student/{studentName}/{format?}")]
        public string GetStudentReport(string studentName, string format = "json")
        {
            StudentReport studentReport = reportGenerator.GetReportByStudent(studentName);
            string result = ReportSerializer<StudentReport>.SerializeReport(studentReport, format);
            return result;
        }

        [HttpGet("report/lecture/{lectureName}/{format?}")]
        public string GetLectureReport(string lectureName, string format = "json")
        {
            LectureReport lectureReport = reportGenerator.GetReportByLecture(lectureName);
            string result = ReportSerializer<LectureReport>.SerializeReport(lectureReport, format);
            return result;
        }
    }
}

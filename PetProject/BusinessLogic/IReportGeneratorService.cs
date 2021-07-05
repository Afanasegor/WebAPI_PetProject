using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IReportGeneratorService
    {
        public StudentReport GetReportByStudent(string studentName);
        public LectureReport GetReportByLecture(string lectureName);
    }
}

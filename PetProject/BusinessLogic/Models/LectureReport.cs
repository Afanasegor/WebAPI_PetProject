using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    [Serializable]
    public class LectureReport : IReport
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }

        public List<StudentsAttendanceReport> StudentsAttendenceReport { get; set; }

        public LectureReport()
        {
            this.StudentsAttendenceReport = new List<StudentsAttendanceReport>();
        }
    }
    public class StudentsAttendanceReport
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public bool AttendanceResult { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    [Serializable]
    public class StudentReport : IReport
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public List<AttendanceReport> Attendances { get; set; }

        public StudentReport()
        {
            Attendances = new List<AttendanceReport>();
        }
    }

    public class AttendanceReport
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public bool AttendanceResult { get; set; }
    }
}

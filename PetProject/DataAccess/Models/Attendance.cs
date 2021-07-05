using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class Attendance
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public bool AttendanceResult { get; set; }
    }
}

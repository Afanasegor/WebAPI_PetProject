using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ILectureProcessService
    {
        public IEnumerable<Attendance> StartLecture(int lectureId);

        public Attendance MarkAttendance(int lectureId, int studentId, int homeworkMark);

        public IEnumerable<Attendance> FinishLecture(int lectureId);
    }
}

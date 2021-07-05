using DataAccess;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using CustomExceptions;
using Newtonsoft.Json;

namespace BusinessLogic
{
    public class ReportGenerator : IReportGeneratorService
    {
        private readonly IService<Student> studentService;
        private readonly IService<Lecturer> lecturerService;
        private readonly IService<Lecture> lectureService;
        private readonly IService<Homework> homeworkService;

        public ReportGenerator(IService<Student> studentService,
            IService<Lecturer> lecturerService,
            IService<Lecture> lectureService,
            IService<Homework> homeworkService)
        {
            this.studentService = studentService;
            this.lecturerService = lecturerService;
            this.lectureService = lectureService;
            this.homeworkService = homeworkService;
        }

        public StudentReport GetReportByStudent(string studentName)
        {
            if (studentName == null)
            {
                throw new StudentNotFoundException("Can't find student name (null)...");
            }
            Student student = studentService.GetAll().FirstOrDefault(s => s.FullName.ToLower() == studentName.ToLower());

            if (student == null)
            {
                throw new StudentNotFoundException(studentName);
            }

            StudentReport studentReport = new StudentReport { StudentId = student.Id, StudentName = student.FullName };
            foreach (var attendance in student.Attendances)
            {
                studentReport.Attendances.Add(new AttendanceReport 
                {
                    LectureId = attendance.LectureId,
                    LectureName = attendance.Lecture.Name,
                    AttendanceResult = attendance.AttendanceResult
                });
            }

            return studentReport;
        }

        public LectureReport GetReportByLecture(string lectureName)
        {
            if (lectureName == null)
            {
                throw new LectureNotFoundException("Can't find lecture name (null)...");
            }
            Lecture lecture = lectureService.GetAll().FirstOrDefault(l => l.Name.ToLower() == lectureName.ToLower());

            if (lecture == null)
            {
                throw new LectureNotFoundException(lectureName);
            }

            LectureReport lectureReport = new LectureReport { LectureId = lecture.Id, LectureName = lecture.Name };
            foreach (var attendance in lecture.Attendances)
            {
                lectureReport.StudentsAttendenceReport.Add(new StudentsAttendanceReport
                {
                    StudentId = attendance.StudentId,
                    StudentName = attendance.Student.FullName,
                    AttendanceResult = attendance.AttendanceResult
                });
            }

            return lectureReport;
        }
    }
}

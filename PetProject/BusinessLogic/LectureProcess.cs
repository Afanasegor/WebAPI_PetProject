using CustomExceptions;
using DataAccess;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BusinessLogic
{
    public class LectureProcess : ILectureProcessService
    {
        private readonly IService<Student> studentService;
        private readonly IService<Lecturer> lecturerService;
        private readonly IService<Lecture> lectureService;
        private readonly IService<Homework> homeworkService;
        private readonly INotificationSenderService notificationSender;

        public LectureProcess(IService<Student> studentService, 
            IService<Lecturer> lecturerService, 
            IService<Lecture> lectureService, 
            IService<Homework> homeworkService, INotificationSenderService notificationSender)
        {
            this.studentService = studentService;
            this.lecturerService = lecturerService;
            this.lectureService = lectureService;
            this.homeworkService = homeworkService;
            this.notificationSender = notificationSender;
        }

        public IEnumerable<Attendance> StartLecture(int lectureId)
        {
            Lecture lecture = lectureService.Get(lectureId);

            if (lecture == null)
            {
                throw new LectureNotFoundException(lectureId);
            }

            if (lecture.IsFinished)
            {
                throw new LectureFinishedException(lecture.Id, lecture.Name);
            }

            if (lecture.Attendances.Any())
            {
                throw new LectureStartedException(lecture.Id, lecture.Name);
            }

            var students = studentService.GetAll();

            foreach (var student in students)
            {
                student.Lectures.Add(lecture);

                student.Homeworks.Add(new Homework { Student = student, Lecture = lecture });

                Attendance attendanceOfStudent = new Attendance { Student = student, Lecture = lecture };

                lecture.Attendances.Add(attendanceOfStudent);
            }

            lectureService.Update(lecture);
            lectureService.Save();
            return lecture.Attendances.ToList();
        }

        public Attendance MarkAttendance(int lectureId, int studentId, int homeworkMark = 0)
        {
            Lecture lecture = lectureService.Get(lectureId);

            if (lecture == null)
            {
                throw new LectureNotFoundException(lectureId);
            }

            if (lecture.IsFinished)
            {
                throw new LectureFinishedException(lecture.Id, lecture.Name);
            }

            if (!lecture.Attendances.Any())
            {
                throw new AttendanceNotFoundException($"Attendence for Lecture ({lecture.Id}_{lecture.Name}) doesn't exist... (Most likely lecture is not started)");
            }

            if (lecture.Students.FirstOrDefault(s => s.Id == studentId) == null)
            {
                throw new StudentNotFoundException(studentId);
            }

            if (!Validator.HomeworkMarkIsValid(homeworkMark))
            {
                throw new InvalidHomeworkMarkException(homeworkMark);
            }

            lecture.Attendances.FirstOrDefault(a => a.StudentId == studentId).AttendanceResult = AttendanceConverter.GetAttendanceToBoolean(AttendanceType.present);
            lecture.Homeworks.FirstOrDefault(h => h.StudentId == studentId).Mark = homeworkMark;
            lectureService.Save();

            return lecture.Attendances.FirstOrDefault(a => a.StudentId == studentId);
        }

        public IEnumerable<Attendance> FinishLecture(int lectureId)
        {
            Lecture lecture = lectureService.Get(lectureId);

            if (lecture == null)
            {
                throw new LectureNotFoundException(lectureId);
            }

            if (lecture.IsFinished)
            {
                throw new LectureFinishedException(lecture.Id, lecture.Name);
            }

            if (!lecture.Attendances.Any())
            {
                throw new AttendanceNotFoundException($"Attendence for Lecture ({lecture.Id}_{lecture.Name}) doesn't exist... (Most likely lecture is not started)");
            }

            notificationSender.NotifyAllAboutAttendance();
            
            notificationSender.NotifyAllAboutProgress();

            lecture.IsFinished = true;
            lectureService.Update(lecture);
            lectureService.Save();


            return lecture.Attendances.ToList();
        }
    }
}
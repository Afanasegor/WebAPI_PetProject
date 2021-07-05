using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomExceptions;
using DataAccess;
using Service;

namespace BusinessLogic
{
    public class NotificationSender : INotificationSenderService
    {
        private readonly IService<Student> studentService;
        private readonly IService<Lecture> lectureService;

        public NotificationSender(IService<Student> studentService, IService<Lecture> lectureService)
        {
            this.studentService = studentService;
            this.lectureService = lectureService;
        }
        public List<EmailNotificationModel> NotifyAllAboutAttendance()
        {
            List<EmailNotificationModel> allNotifications = new List<EmailNotificationModel>();

            List<Student> allStudents = studentService.GetAll().ToList();
            List<Student> truantsList = new List<Student>();

            foreach (var student in allStudents)
            {
                if (CheckAttendance(student))
                {
                    truantsList.Add(student);

                    allNotifications.Add(new EmailNotificationModel 
                    {
                        Email = student.Email,
                        Message = $"{student.FullName}, you skipped more than 2 lectures..."
                    });
                }
            }

            if (truantsList.Count > 0)
            {
                Lecture lastLecture = lectureService.GetAll().Where(l => l.IsFinished).Last();

                if (lastLecture == null)
                {
                    throw new LectureNotFoundException();
                }

                Lecturer lecturer = lastLecture.Lecturer;

                if (lecturer == null)
                {
                    throw new LecturerNotFoundException();
                }

                var studentNameArray = truantsList.Select(s => s.FullName).ToArray();
                string message = $"{lecturer.FullName}, here is list of students, who skipped more than 2 lectures: {string.Join(", ", studentNameArray)}";

                allNotifications.Add(new EmailNotificationModel { Email = lecturer.Email, Message = message });
            }

            return allNotifications;
        }

        public List<SmsNotificationModel> NotifyAllAboutProgress()
        {
            List<SmsNotificationModel> allNotifications = new List<SmsNotificationModel>();

            List<Student> allStudents = studentService.GetAll().ToList();
            foreach (var student in allStudents)
            {
                if (CheckStudentProgress(student))
                {
                    string message = $"{student.FullName}, you average mark is less than 4...";
                    allNotifications.Add(new SmsNotificationModel { PhoneNumber = student.PhoneNumber, Message = message });
                }
            }

            return allNotifications;
        }

        public static bool CheckAttendance(Student student)
        {
            int skippedLectureCount = 0;
            foreach (var attendance in student.Attendances)
            {
                if (attendance.AttendanceResult == false)
                {
                    skippedLectureCount++;
                }
            }
            if (skippedLectureCount > 2)
            {
                return true;
            }
            return false;
        }

        public static bool CheckStudentProgress(Student student)
        {
            double averageMark;
            int sumOfMarks = 0;

            foreach (var homework in student.Homeworks)
            {
                sumOfMarks += homework.Mark;
            }

            if (student.Homeworks.Count == 0)
            {
                throw new HomeworkNotFoundException($"Student ({student.Id}_{student.FullName}) doesn't have homeworks");
            }
            else
            {
                averageMark = sumOfMarks / student.Homeworks.Count;
            }

            if (averageMark < 4)
            {
                return true;
            }
            return false;
        }
    }

    public class EmailNotificationModel
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }

    public class SmsNotificationModel
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}

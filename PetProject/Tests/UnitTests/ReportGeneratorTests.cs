using BusinessLogic;
using CustomExceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Repository;
using Service;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class ReportGeneratorTests
    {
        public ReportGenerator reportGenerator;

        private Mock<IService<Student>> mockStudentService;
        private Mock<IService<Lecturer>> mockLecturerService;
        private Mock<IService<Lecture>> mockLectureService;
        private Mock<IService<Homework>> mockHomeworkService;

        [SetUp]
        public void InitializeServices()
        {
            mockStudentService = new Mock<IService<Student>>();

            mockLecturerService = new Mock<IService<Lecturer>>();

            mockLectureService = new Mock<IService<Lecture>>();

            mockHomeworkService = new Mock<IService<Homework>>();
        }

        private void InitializeReportGenerator()
        {
            reportGenerator = new ReportGenerator(
                mockStudentService.Object, 
                mockLecturerService.Object, 
                mockLectureService.Object, 
                mockHomeworkService.Object);
        }




        [TestCase(null)]
        [TestCase("")]
        [TestCase("Xcvnkl")]
        [TestCase("Andrey Konovalov")]
        public void GetReportByStudentTest_NonExistentStudent_StudentNotFoundException(string studentName)
        {
            // arrange
            var students = new Student[]
            {
                new Student("Egor", "Afanasyev", "89274690937") { Id = 1},
                new Student("Bulat", "Zakirov", "81234690912") { Id = 2},
                new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}
            };
            mockStudentService.Setup(service => service.GetAll()).Returns(students);
            InitializeReportGenerator();

            // act & assert
            Assert.Throws<StudentNotFoundException>(() => reportGenerator.GetReportByStudent(studentName));
        }

        [TestCase("Egor Afanasyev")]
        public void GetReportByStudentTest_ExistentStudent_CorrectReportModel(string studentName)
        {
            // arrange
            var lecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = true
            };
            var students = new Student[]
            {
                new Student("Egor", "Afanasyev", "89274690937") { Id = 1,
                    Lectures = new List<Lecture>(),
                    Homeworks = new List<Homework> { new Homework {Id = 1, LectureId = 1, StudentId = 1, Mark = 5 } },
                    Attendances = new List<Attendance> { new Attendance { StudentId = 1, LectureId = 1, Lecture = lecture, AttendanceResult = true} } },
                new Student("Bulat", "Zakirov", "81234690912") { Id = 2,
                    Lectures = new List<Lecture>(),
                    Homeworks = new List<Homework> { new Homework {Id = 2, LectureId = 1, StudentId = 2, Mark = 4 } },
                    Attendances = new List<Attendance> { new Attendance { StudentId = 1, LectureId = 1, Lecture = lecture, AttendanceResult = true} }},
                new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3,
                    Lectures = new List<Lecture>(),
                    Homeworks = new List<Homework> { new Homework {Id = 3, LectureId = 1, StudentId = 3, Mark = 0 } },
                    Attendances = new List<Attendance> { new Attendance { StudentId = 1, LectureId = 1, Lecture = lecture, AttendanceResult = false} }}
            };
            mockStudentService.Setup(service => service.GetAll()).Returns(students);
            InitializeReportGenerator();

            // act
            StudentReport expectedReport = new StudentReport
            {
                StudentId = 1,
                StudentName = "Egor Afanasyev",
                Attendances = new List<AttendanceReport> 
                {
                    new AttendanceReport {LectureId = 1, AttendanceResult = true, LectureName = "Math"}
                }
            };
            StudentReport actualReport = reportGenerator.GetReportByStudent(studentName);

            // assert
            Assert.IsTrue(expectedReport.StudentId == actualReport.StudentId &&
                expectedReport.StudentName == actualReport.StudentName &&
                expectedReport.Attendances.Count == actualReport.Attendances.Count);
        }




        [TestCase(null)]
        [TestCase("")]
        [TestCase("Geography")]
        [TestCase("148noinsad")]
        public void GetReportByLecture_NonExistentLecture_LectureNotFoundException(string lectureName)
        {
            // arrange
            var student1 = new Student { Id = 1, Name = "Egor", Surname = "Afanasyev" };
            var student2 = new Student { Id = 2, Name = "Bulat", Surname = "Zakirov" };
            var student3 = new Student { Id = 3, Name = "Mikhail", Surname = "Ibragimov" };

            var lectures = new Lecture[]
            {
                new Lecture
                {
                    Id = 1,
                    Attendances = new List<Attendance>
                    {
                        new Attendance { StudentId = 1, Student = student1, LectureId = 1, AttendanceResult = true},
                        new Attendance { StudentId = 2, Student = student2, LectureId = 1, AttendanceResult = true},
                        new Attendance { StudentId = 3, Student = student3, LectureId = 1, AttendanceResult = false},
                    },
                    Name = "Math"
                },
                new Lecture
                {
                    Id = 2,
                    Attendances = new List<Attendance>
                    {
                        new Attendance { StudentId = 1, Student = student1, LectureId = 1, AttendanceResult = true},
                        new Attendance { StudentId = 2, Student = student2, LectureId = 1, AttendanceResult = false},
                        new Attendance { StudentId = 3, Student = student3, LectureId = 1, AttendanceResult = false},
                    },
                    Name = "It"
                }
            };

            mockLectureService.Setup(service => service.GetAll()).Returns(lectures);
            InitializeReportGenerator();

            // act & assert
            Assert.Throws<LectureNotFoundException>(() => reportGenerator.GetReportByLecture(lectureName));
        }

        [TestCase("Math")]
        public void GetReportByLecture_ExistentLecture_CorrectReportModel(string lectureName)
        {
            // arrange
            var student1 = new Student { Id = 1, Name = "Egor", Surname = "Afanasyev" };
            var student2 = new Student { Id = 2, Name = "Bulat", Surname = "Zakirov" };
            var student3 = new Student { Id = 3, Name = "Mikhail", Surname = "Ibragimov" };

            var lectures = new Lecture[]
            {
                new Lecture
                {
                    Id = 1,
                    Attendances = new List<Attendance>
                    {
                        new Attendance { StudentId = 1, Student = student1, LectureId = 1, AttendanceResult = true},
                        new Attendance { StudentId = 2, Student = student2, LectureId = 1, AttendanceResult = true},
                        new Attendance { StudentId = 3, Student = student3, LectureId = 1, AttendanceResult = false},
                    },
                    Name = "Math"
                },
                new Lecture
                {
                    Id = 2,
                    Attendances = new List<Attendance>
                    {
                        new Attendance { StudentId = 1, Student = student1, LectureId = 1, AttendanceResult = true},
                        new Attendance { StudentId = 2, Student = student2, LectureId = 1, AttendanceResult = false},
                        new Attendance { StudentId = 3, Student = student3, LectureId = 1, AttendanceResult = false},
                    },
                    Name = "It"
                }
            };

            mockLectureService.Setup(service => service.GetAll()).Returns(lectures);
            InitializeReportGenerator();

            // act
            LectureReport actualReport = reportGenerator.GetReportByLecture(lectureName);
            LectureReport expectedReport = new LectureReport
            {
                LectureId = 1,
                LectureName = "Math",
                StudentsAttendenceReport = new List<StudentsAttendanceReport>
                {
                    new StudentsAttendanceReport { StudentId = 1, StudentName = "Egor Afanasyev", AttendanceResult = true },
                    new StudentsAttendanceReport { StudentId = 2, StudentName = "Bulat Zakirov", AttendanceResult = true},
                    new StudentsAttendanceReport { StudentId = 3, StudentName = "Mikhail Ibragimov", AttendanceResult = false}
                }
            };

            // assert
            Assert.IsTrue(expectedReport.LectureId == actualReport.LectureId &&
                expectedReport.LectureName == actualReport.LectureName &&
                expectedReport.StudentsAttendenceReport.Count == actualReport.StudentsAttendenceReport.Count);
        }
    }
}

using BusinessLogic;
using CustomExceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Repository;
using Service;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;

namespace Tests
{
    [TestFixture]
    public class BusinessIntegrationTests
    {
        public LectureProcessController lectureProcessController;
        public ReportController reportController;

        private IService<Student> studentService;
        private IService<Lecturer> lecturerService;
        private IService<Lecture> lectureService;
        private IService<Homework> homeworkService;

        ServiceCollection services;

        private ServiceProvider serviceProvider;
        [SetUp]
        public void InitializeServices()
        {
            services = new ServiceCollection();

            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryContext"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IService<Student>, StudentService>();
            services.AddTransient<IService<Lecturer>, LecturerService>();
            services.AddTransient<IService<Lecture>, LectureService>();
            services.AddTransient<IService<Homework>, HomeworkService>();
            services.AddTransient<ILectureProcessService, LectureProcess>();
            services.AddTransient<IReportGeneratorService, ReportGenerator>();

            serviceProvider = services.BuildServiceProvider();

            studentService = serviceProvider.GetService<IService<Student>>();
            lecturerService = serviceProvider.GetService<IService<Lecturer>>();
            lectureService = serviceProvider.GetService<IService<Lecture>>();
            homeworkService = serviceProvider.GetService<IService<Homework>>();


            lectureProcessController = new LectureProcessController(serviceProvider.GetService<ILectureProcessService>());
            reportController = new ReportController(serviceProvider.GetService<IReportGeneratorService>());
        }

        [Order(1)]
        [TestCase(1)]
        public void StartLectureTest_CorrectLectureId_CorrectResult(int lectureId)
        {
            using (ApplicationContext context = serviceProvider.GetService<ApplicationContext>())
            {
                // act
                lectureProcessController.StartLecture(lectureId);
                var lecture = lectureService.Get(lectureId);
                // assert
                Assert.IsTrue(lecture.Attendances.Any());
            }
        }

        [Order(2)]
        [TestCase(1,1,5)]
        public void MarkAttendanceTest_CorrectData_CorrectResult(int lectureId, int studentId, int homeworkMark)
        {
            using (ApplicationContext context = serviceProvider.GetService<ApplicationContext>())
            {
                // arrange
                var expectedAttendance = new Attendance { LectureId = 1, StudentId = 1, AttendanceResult = true };

                // act
                lectureProcessController.MarkAttendance(lectureId, studentId, homeworkMark);
                var actualAttendance = studentService.Get(studentId).Attendances.FirstOrDefault(a => a.LectureId == lectureId);

                // assert
                Assert.IsTrue(expectedAttendance.StudentId == actualAttendance.StudentId &&
                    expectedAttendance.LectureId == actualAttendance.LectureId &&
                    expectedAttendance.AttendanceResult == actualAttendance.AttendanceResult);
            }
        }

        [Order(3)]
        [TestCase(1)]
        public void FinishLectureTest_CorrectData_CorrectResult(int lectureId)
        {
            // arrange
            var expectedLecture = new Lecture
            {
                Id = lectureId,
                Attendances = new Attendance[]
                {
                    new Attendance { LectureId = lectureId, StudentId = 1, AttendanceResult = true },
                    new Attendance { LectureId = lectureId, StudentId = 1, AttendanceResult = true },
                    new Attendance { LectureId = lectureId, StudentId = 1, AttendanceResult = false },
                    new Attendance { LectureId = lectureId, StudentId = 1, AttendanceResult = false },
                },
                IsFinished = true
            };

            // act

            lectureProcessController.FinishLecture(lectureId);

            var actualLecture = lectureService.Get(lectureId);
            // assert
            Assert.IsTrue(expectedLecture.Id == actualLecture.Id &&
                expectedLecture.Attendances.Count == actualLecture.Attendances.Count &&
                expectedLecture.IsFinished == actualLecture.IsFinished);
        }

        [Order(4)]
        [TestCase("Egor Afanasyev", "json")]
        [TestCase("Egor Afanasyev", "xml")]
        public void GetStudentReportTest_CorrectStudentName_CorrectReport(string studentName, string format)
        {
            // arrange
            StudentReport correctStudentReport = new StudentReport
            {
                StudentId = 1,
                StudentName = studentName,
                Attendances = new List<AttendanceReport>
                {
                    new AttendanceReport { LectureId = 1, LectureName = "Algorithms", AttendanceResult = true}
                }
            };
            var expectedReport = ReportSerializer<StudentReport>.SerializeReport(correctStudentReport, format);

            // act
            string actualReport = reportController.GetStudentReport(studentName, format);

            // assert
            Assert.AreEqual(expectedReport, actualReport);
        }
    }
}

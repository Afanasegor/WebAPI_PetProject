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
    public class LectureProcessTests
    {
        public LectureProcess lectureProcess;

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

        private void InitializeLectureProcess()
        {
            lectureProcess = new LectureProcess(mockStudentService.Object,
                mockLecturerService.Object,
                mockLectureService.Object,
                mockHomeworkService.Object);
        }


        [Test]
        public void StartLectureTest_NonExistentLectureId_NotFoundException()
        {
            // arrange
            int lectureId = 10;

            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureNotFoundException>(() => lectureProcess.StartLecture(lectureId));
        }

        [Test]
        public void StartLectureTest_FinishedLecture_LectureFinishedException()
        {
            // arrange                
            int lectureId = 1;
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = true,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1},
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2},
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = true}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureFinishedException>(() => lectureProcess.StartLecture(lectureId));
        }

        [Test]
        public void StartLectureTest_LectureAlreadyWithAttendances_LectureStartedException()
        {
            // arrange                
            int lectureId = 1;
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1},
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2},
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = true}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureStartedException>(() => lectureProcess.StartLecture(lectureId));
        }

        [Test]
        public void StartLectureTest_LectureWithAttendances_LectureStartedException()
        {
            // arrange                
            int lectureId = 1;
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1},
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2},
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = true}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureStartedException>(() => lectureProcess.StartLecture(lectureId));
        }

        [Test]
        public void StartLectureTest_LectureId_CorrectLectureStart()
        {
            // arrange                
            int lectureId = 1;
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1, 
                        Lectures = new List<Lecture>(), 
                        Homeworks = new List<Homework>() },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2, 
                        Lectures = new List<Lecture>(), 
                        Homeworks = new List<Homework>() },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3, 
                        Lectures = new List<Lecture>(), 
                        Homeworks = new List<Homework>() }
                },                
                Attendances = new List<Attendance>()
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            mockStudentService.Setup(service => service.GetAll()).Returns(returnLecture.Students);

            InitializeLectureProcess();

            // act
            var startLectureResult = lectureProcess.StartLecture(lectureId).Count();

            // act & assert
            Assert.AreEqual(returnLecture.Students.Count, startLectureResult);
        }





        [TestCase(10, 1)]
        public void MarkAttendanceTest_NonExistentLectureId_LectureNotFoundException(int lectureId, int studentId)
        {
            // arrange
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureNotFoundException>(() => lectureProcess.MarkAttendance(lectureId, studentId));
        }

        [TestCase(1, 1)]
        public void MarkAttendanceTest_FinishedLecture_LectureFinishedException(int lectureId, int studentId)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = true,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1},
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2},
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = true}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureFinishedException>(() => lectureProcess.MarkAttendance(lectureId, studentId));
        }

        [TestCase(1, 1)]
        public void MarkAttendanceTest_NotStartedLecture_AttendanceNotFoundException(int lectureId, int studentId)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1 },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2 },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3 }
                },
                Attendances = new List<Attendance>()
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            InitializeLectureProcess();

            // act & assert
            Assert.Throws<AttendanceNotFoundException>(() => lectureProcess.MarkAttendance(lectureId, studentId));
        }

        [TestCase(1, 4)]
        public void MarkAttendanceTest_NonExistentStudentId_StudentNotFoundException(int lectureId, int studentId)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1 },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2 },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3 }
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = false}
                },
                Homeworks = new Homework[]
                {
                    new Homework {Id = 1, StudentId = 1, LectureId = 1},
                    new Homework {Id = 2, StudentId = 1, LectureId = 1},
                    new Homework {Id = 3, StudentId = 1, LectureId = 1}
                }
            };
            foreach (var attendanceOfLecture in returnLecture.Attendances)
            {
                attendanceOfLecture.Lecture = returnLecture;
            }
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            InitializeLectureProcess();


            // act & assert
            Assert.Throws<StudentNotFoundException>(() => lectureProcess.MarkAttendance(lectureId, studentId));
        }

        [TestCase(1, 1, 10)]
        [TestCase(1, 1, -10)]
        public void MarkAttendanceTest_IncorrectHomeworkMark_InvalidHomeworkMark(int lectureId, int studentId, int homeworkMark)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1 },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2 },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3 }
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = false}
                },
                Homeworks = new Homework[]
                {
                    new Homework {Id = 1, StudentId = 1, LectureId = 1},
                    new Homework {Id = 2, StudentId = 1, LectureId = 1},
                    new Homework {Id = 3, StudentId = 1, LectureId = 1}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            InitializeLectureProcess();

            // act & assert
            Assert.Throws<InvalidHomeworkMarkException>(() => lectureProcess.MarkAttendance(lectureId, studentId, homeworkMark));
        }

        [TestCase(1, 1)]
        public void MarkAttendanceTest_MarkAttendanceWithoutHomework_CorrectResult(int lectureId, int studentId)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1 },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2 },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3 }
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = false}
                },
                Homeworks = new Homework[]
                {
                    new Homework {Id = 1, StudentId = 1, LectureId = 1},
                    new Homework {Id = 2, StudentId = 1, LectureId = 1},
                    new Homework {Id = 3, StudentId = 1, LectureId = 1}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            InitializeLectureProcess();

            // act
            var actualAttendance = lectureProcess.MarkAttendance(lectureId, studentId);
            var expectedAttendance = new Attendance { StudentId = studentId, LectureId = lectureId, AttendanceResult = true };


            // assert
            Assert.AreEqual(expectedAttendance.AttendanceResult, actualAttendance.AttendanceResult);
        }

        [TestCase(1,1,5)]
        public void MarkAttendanceTest_MarkAttendanceWithHomework_CorrectResult(int lectureId, int studentId, int homeworkMark)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1 },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2 },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3 }
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = false},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = false}
                },
                Homeworks = new Homework[]
                {
                    new Homework {Id = 1, StudentId = 1, LectureId = 1},
                    new Homework {Id = 2, StudentId = 1, LectureId = 1},
                    new Homework {Id = 3, StudentId = 1, LectureId = 1}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            mockLectureService.Setup(service => service.Save()).Callback(() => returnLecture.Homeworks.FirstOrDefault(h => h.StudentId == studentId).Mark = homeworkMark);

            InitializeLectureProcess();

            // act
            lectureProcess.MarkAttendance(lectureId, studentId);
            var actualHomeworkMark = returnLecture.Homeworks.FirstOrDefault(h => h.StudentId == studentId).Mark;
            var expectedHomeworkMark = homeworkMark;

            // assert
            Assert.AreEqual(expectedHomeworkMark, actualHomeworkMark);
        }




        [TestCase(10)]
        public void FinishLectureTest_NonExistentLectureId_LectureNotFoundException(int lectureId)
        {
            // arrange
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureNotFoundException>(() => lectureProcess.FinishLecture(lectureId));
        }

        [TestCase(1)]
        public void FinishLectureTest_FinishedLecture_LectureFinishedException(int lectureId)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = true,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1},
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2},
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = true}
                }
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);
            InitializeLectureProcess();

            // act & assert
            Assert.Throws<LectureFinishedException>(() => lectureProcess.FinishLecture(lectureId));
        }

        [TestCase(1)]
        public void FinishLectureTest_NotStartedLecture_AttendanceNotFoundException(int lectureId)
        {
            // arrange
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1 },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2 },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3 }
                },
                Attendances = new List<Attendance>()
            };
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);

            InitializeLectureProcess();

            // act & assert
            Assert.Throws<AttendanceNotFoundException>(() => lectureProcess.FinishLecture(lectureId));
        }

        [TestCase(1)]
        public void FinishLectureTest_LectureId_CorrectLectureFinish(int lectureId)
        {
            // arrange
            bool lectureIsFinished = false;
            Lecture returnLecture = new Lecture
            {
                Id = 1,
                Name = "Math",
                IsFinished = false,
                LecturerId = 1,
                Lecturer = new Lecturer { Id = 1, Name = "Ivan", Surname = "Ivanov", Email = "ivan.ivanov.lecturer@epam.com"},
                Students = new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1,
                        Lectures = new List<Lecture>(),
                        Homeworks = new List<Homework> { new Homework {Id = 1, LectureId = 1, StudentId = 1, Mark = 5 } }, 
                        Attendances = new List<Attendance> { new Attendance { StudentId = 1, LectureId = 1, AttendanceResult = true} } },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2,
                        Lectures = new List<Lecture>(),
                        Homeworks = new List<Homework> { new Homework {Id = 2, LectureId = 1, StudentId = 2, Mark = 4 } },
                        Attendances = new List<Attendance> { new Attendance { StudentId = 1, LectureId = 1, AttendanceResult = true} }},
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3,
                        Lectures = new List<Lecture>(),
                        Homeworks = new List<Homework> { new Homework {Id = 3, LectureId = 1, StudentId = 3, Mark = 0 } },
                        Attendances = new List<Attendance> { new Attendance { StudentId = 1, LectureId = 1, AttendanceResult = false} }}
                },
                Attendances = new Attendance[]
                {
                    new Attendance {StudentId = 1, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 2, LectureId = 1, AttendanceResult = true},
                    new Attendance {StudentId = 3, LectureId = 1, AttendanceResult = false}
                },
                Homeworks = new Homework[]
                {
                    new Homework {Id = 1, StudentId = 1, LectureId = 1},
                    new Homework {Id = 2, StudentId = 1, LectureId = 1},
                    new Homework {Id = 3, StudentId = 1, LectureId = 1}
                }
            };
            
            mockLectureService.Setup(service => service.Get(lectureId)).Returns(returnLecture);
            mockLectureService.Setup(service => service.Save()).Callback(() => lectureIsFinished = true);
            InitializeLectureProcess();

            // act
            lectureProcess.FinishLecture(lectureId);

            // assert
            Assert.AreEqual(true, lectureIsFinished);
        }
    }
}
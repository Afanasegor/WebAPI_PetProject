using CustomExceptions;
using DataAccess;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class StudentService : IService<Student>
    {
        private IRepository<Student> studentRepository;
        
        public StudentService(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetAll()
        {
            return studentRepository.GetAll();
        }
        public Student Get(int id)
        {
            Student student = studentRepository.Get(id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }
            return student;
        }

        public void Create(Student student)
        {
            if (studentRepository.Get(student.Id) != null)
            {
                throw new UpdateDataBaseException($"Student with Id ({student.Id}) already exists...");
            }

            Validator.ValidateStudent(student);

            studentRepository.Create(student);
        }

        public void Update(Student student)
        {
            Validator.ValidateStudent(student);

            studentRepository.Update(student);
        }

        public void Delete(int id)
        {
            if (studentRepository.Get(id) == null)
            {
                throw new StudentNotFoundException(id);
            }

            studentRepository.Delete(id);
        }

        public void Save()
        {
            studentRepository.Save();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CustomExceptions;

namespace Service
{
    public class LectureService : IService<Lecture>
    {
        private IRepository<Lecture> lectureRepository;

        public LectureService(IRepository<Lecture> lectureRepository)
        {
            this.lectureRepository = lectureRepository;
        }

        public IEnumerable<Lecture> GetAll()
        {
            return lectureRepository.GetAll();
        }

        public Lecture Get(int id)
        {
            Lecture lecture = lectureRepository.Get(id);

            if (lecture == null)
            {
                throw new LectureNotFoundException(id);
            }

            return lecture;
        }

        public void Create(Lecture lecture)
        {
            if (lectureRepository.Get(lecture.Id) != null)
            {
                throw new UpdateDataBaseException($"Lecture with Id ({lecture.Id}) already exists...");
            }
            lectureRepository.Create(lecture);
        }

        public void Update(Lecture lecture)
        {
            lectureRepository.Update(lecture);
        }

        public void Delete(int id)
        {
            if (lectureRepository.Get(id) == null)
            {
                throw new LecturerNotFoundException(id);
            }
            lectureRepository.Delete(id);
        }

        public void Save()
        {
            lectureRepository.Save();
        }
    }
}

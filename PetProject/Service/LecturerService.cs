using CustomExceptions;
using DataAccess;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class LecturerService : IService<Lecturer>
    {
        private IRepository<Lecturer> lecturerRepository;
        
        public LecturerService(IRepository<Lecturer> lecturerRepository)
        {
            this.lecturerRepository = lecturerRepository;
        }

        public IEnumerable<Lecturer> GetAll()
        {
            return lecturerRepository.GetAll();
        }
        public Lecturer Get(int id)
        {
            Lecturer lecturer = lecturerRepository.Get(id);

            if (lecturer == null)
            {
                throw new LecturerNotFoundException(id);
            }

            return lecturer;
        }

        public void Create(Lecturer lecturer)
        {
            if (lecturerRepository.Get(lecturer.Id) != null)
            {
                throw new UpdateDataBaseException($"Lecturer with Id ({lecturer.Id}) already exists...");
            }

            Validator.ValidateLecturer(lecturer);

            lecturerRepository.Create(lecturer);
        }

        public void Update(Lecturer lecturer)
        {
            Validator.ValidateLecturer(lecturer);
            
            lecturerRepository.Update(lecturer);
        }

        public void Delete(int id)
        {
            if (lecturerRepository.Get(id) == null)
            {
                throw new LecturerNotFoundException(id);
            }

            lecturerRepository.Delete(id);
        }

        public void Save()
        {
            lecturerRepository.Save();
        }
    }
}

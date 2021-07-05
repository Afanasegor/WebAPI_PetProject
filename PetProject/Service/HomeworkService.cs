using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using DataAccess;
using CustomExceptions;

namespace Service
{
    public class HomeworkService : IService<Homework>
    {
        private IRepository<Homework> homeworkRepository;

        public HomeworkService(IRepository<Homework> homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        public IEnumerable<Homework> GetAll()
        {
            return homeworkRepository.GetAll();
        }
        public Homework Get(int id)
        {
            Homework homework = homeworkRepository.Get(id);

            if (homework == null)
            {
                throw new HomeworkNotFoundException(id);
            }

            return homework;
        }

        public void Create(Homework homework)
        {
            if (homeworkRepository.Get(homework.Id) != null)
            {
                throw new UpdateDataBaseException($"Homework with Id ({homework.Id}) already exists...");
            }
            homeworkRepository.Create(homework);
        }

        public void Update(Homework homework)
        {

            homeworkRepository.Update(homework);
        }

        public void Delete(int id)
        {
            if (homeworkRepository.Get(id) == null)
            {
                throw new HomeworkNotFoundException(id);
            }
            homeworkRepository.Delete(id);
        }

        public void Save()
        {
            homeworkRepository.Save();
        }
    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IService<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Create(T lecturer);
        void Update(T lecturer);
        void Delete(int id);
        void Save();
    }
}

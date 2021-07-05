using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ISerializer<T> where T : IReport
    {
        public string Serialize(T report);
    }
}

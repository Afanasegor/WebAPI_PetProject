using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class UpdateDataBaseException : CustomException
    {
        public UpdateDataBaseException()
        {

        }
        public UpdateDataBaseException(string message)
            : base (message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class InvalidEmailException : CustomException
    {
        public InvalidEmailException()
        {

        }
        public InvalidEmailException(string email)
            : base ($"This Email ({email}) is not valid...")
        {

        }
    }
}

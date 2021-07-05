using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class InvalidLecturerException : CustomException
    {
        public InvalidLecturerException()
        {

        }

        public InvalidLecturerException(string message)
            : base (message)
        {

        }

        public InvalidLecturerException(string message, string propertyName)
            : base ($"{message} Property Name: {propertyName}")
        {

        }
    }
}

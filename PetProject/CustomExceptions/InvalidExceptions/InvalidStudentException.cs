using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class InvalidStudentException : CustomException
    {
        public InvalidStudentException()
        {

        }
        public InvalidStudentException(string message)
            : base($"{message}")
        {

        }
        public InvalidStudentException(string message, string propertyName)
            : base ($"{message} Property Name: {propertyName}")
        {

        }
    }
}

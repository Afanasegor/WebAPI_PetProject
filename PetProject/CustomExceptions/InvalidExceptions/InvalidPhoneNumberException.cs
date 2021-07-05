using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class InvalidPhoneNumberException : CustomException
    {
        public InvalidPhoneNumberException()
        {

        }
        public InvalidPhoneNumberException(string phoneNumber)
            : base ($"This Phone Number ({phoneNumber}) is not valid...")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class InvalidHomeworkMarkException : CustomException
    {
        public InvalidHomeworkMarkException()
        {

        }

        public InvalidHomeworkMarkException(int homeworkMark)
            : base($"This homework mark ({homeworkMark}) is not valid... It must be in range 0-5")
        {

        }
    }
}

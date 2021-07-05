using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class HomeworkNotFoundException : CustomException
    {
        public HomeworkNotFoundException()
        {

        }
        public HomeworkNotFoundException(int homeworkId)
            : base($"Homework with Id ({homeworkId}) is not found...")
        {

        }
        public HomeworkNotFoundException(string message)
            : base(message)
        {
            
        }
    }
}

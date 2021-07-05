using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomExceptions
{
    [Serializable]
    public class StudentNotFoundException : CustomException
    {
        public StudentNotFoundException() 
        {
            
        }

        public StudentNotFoundException(int studentId)
            : base($"Student with Id ({studentId}) is not found...")
        {

        }
        public StudentNotFoundException(string name)
            :base($"Student with Name ({name}) is not found...")
        {

        }
    }
}

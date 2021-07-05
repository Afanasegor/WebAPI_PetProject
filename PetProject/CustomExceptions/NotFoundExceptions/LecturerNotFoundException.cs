using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class LecturerNotFoundException : CustomException
    {
        public LecturerNotFoundException()
        {

        }
        public LecturerNotFoundException(int lecturerId)
            : base($"Lecturer with Id ({lecturerId}) is not found...")
        {

        }
        public LecturerNotFoundException(string name)
            : base($"Lecturer with Name ({name}) is not found...")
        {

        }
    }
}

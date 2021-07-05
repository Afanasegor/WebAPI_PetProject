using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class LectureNotFoundException : CustomException
    {
        public LectureNotFoundException()
        {

        }
        public LectureNotFoundException(int lectureId)
            : base($"Lecture with Id ({lectureId}) is not found...")
        {

        }

        public LectureNotFoundException(string name)
            : base($"Lecture with Name ({name}) is not found")
        {

        }
    }
}

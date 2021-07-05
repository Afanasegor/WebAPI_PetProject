using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class LectureStartedException : CustomException
    {
        public LectureStartedException()
        {

        }
        public LectureStartedException(int lectureId, string lectureName)
            : base($"Lecture {lectureId}_{lectureName} has been already started...")
        {

        }
    }
}

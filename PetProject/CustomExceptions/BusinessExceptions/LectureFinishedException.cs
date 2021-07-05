using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class LectureFinishedException : CustomException
    {
        public LectureFinishedException()
        {

        }
        public LectureFinishedException(int lectureId, string lectureName)
            : base ($"Lecture {lectureId}_{lectureName} has been already finished...")
        {

        }
    }
}

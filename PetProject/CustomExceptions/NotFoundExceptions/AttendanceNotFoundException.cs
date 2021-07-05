using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class AttendanceNotFoundException : CustomException
    {
        public AttendanceNotFoundException()
        {

        }
        public AttendanceNotFoundException(string message)
            : base (message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    [Serializable]
    public class EntityNullException : CustomException
    {
        public EntityNullException()
        {

        }
        public EntityNullException(string message)
            : base (message)
        {

        }
    }
}

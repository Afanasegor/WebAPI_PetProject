using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomExceptions
{
    public class CustomException : Exception
    {
        public CustomException()
        {

        }
        public CustomException(string message)
            :base (message)
        {

        }
        public override string StackTrace
        {
            get
            {
                List<string> stackTrace = new List<string>();
                stackTrace.AddRange(base.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
                var response = stackTrace.Where(s => s.Contains(":line"));
                return string.Join(Environment.NewLine, response.ToArray());
            }
        }
    }
}

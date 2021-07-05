using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BusinessLogic
{
    public class XmlReportSerializer<T> : ISerializer<T> where T : IReport
    {
        public string Serialize(T report)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));

            using (StringWriter sw = new StringWriter())
            {
                formatter.Serialize(sw, report);
                return sw.ToString();
            }
        }
    }
}

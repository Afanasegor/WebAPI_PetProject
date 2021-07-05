using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace BusinessLogic
{
    public class ReportSerializer<T> where T : IReport
    {
        private static Dictionary<string, ISerializer<T>> serializeFormats = new Dictionary<string, ISerializer<T>>
        {
            {"json", new JsonReportSerializer<T>() },
            {"xml", new XmlReportSerializer<T>() }
        };
        public static string SerializeReport(T report, string format)
        {
            ISerializer<T> serializer;
            serializeFormats.TryGetValue(format.ToLower(), out serializer);

            if (serializer == null)
            {
                throw new ArgumentException($"This format ({format}) doesn't exist", "format");
            }

            string result = serializer.Serialize(report);

            return result;
        }
    }
}

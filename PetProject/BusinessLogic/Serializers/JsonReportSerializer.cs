using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace BusinessLogic
{
    public class JsonReportSerializer<T> : ISerializer<T> where T : IReport
    {
        public string Serialize(T report)
        {
            //string result = System.Text.Json.JsonSerializer.Serialize(report);

            string result = JsonConvert.SerializeObject(report, Formatting.Indented);
            return result;
        }
    }
}

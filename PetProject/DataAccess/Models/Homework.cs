using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess
{
    public class Homework : BaseEntity
    {
        public int Mark { get; set; }


        public int StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }

        public int LectureId { get; set; }
        [JsonIgnore]
        public virtual Lecture Lecture { get; set; }
    }
}

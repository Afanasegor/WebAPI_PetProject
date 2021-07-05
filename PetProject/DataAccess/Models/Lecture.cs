using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess
{
    public class Lecture : BaseEntity
    {
        public string Name { get; set; }

        public bool IsFinished { get; set; }
        public int LecturerId { get; set; }
        [JsonIgnore]
        public virtual Lecturer Lecturer { get; set; }

        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }

        [JsonIgnore]
        public virtual ICollection<Homework> Homeworks { get; set; }

        [JsonIgnore]
        public virtual ICollection<Attendance> Attendances { get; set; }

        //public Lecture()
        //{
        //    Students = new List<Student>();
        //    Homeworks = new List<Homework>();
        //    Attendances = new List<Attendance>();
        //}
    }
}

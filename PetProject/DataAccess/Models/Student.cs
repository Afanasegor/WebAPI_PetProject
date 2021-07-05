using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string FullName => $"{Name} {Surname}";

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        [JsonIgnore]
        public virtual ICollection<Lecture> Lectures { get; set; }

        [JsonIgnore]
        public virtual ICollection<Homework> Homeworks { get; set; }

        [JsonIgnore]
        public virtual ICollection<Attendance> Attendances { get; set; }

        public Student()
        {

        }
        public Student(string name, string surname, string phoneNumber)
        {
            Name = name;
            Surname = surname;

            PhoneNumber = phoneNumber;

            Email = Name.ToLower() + "." + Surname.ToLower() + "@epam.com";
        }
    }
}

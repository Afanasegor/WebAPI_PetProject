using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess
{
    public class Lecturer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string FullName => $"{Name} {Surname}";

        public string Email { get; set; }



        [JsonIgnore]
        public virtual ICollection<Lecture> Lectures { get; set; }

        public Lecturer()
        {

        }
        public Lecturer(string name, string surname)
        {
            Name = name;
            Surname = surname;

            Email = Name.ToLower() + "." + Surname + ".lecturer@epam.com";
        }

        public Lecturer(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;

            Email = email;
        }
    }
}

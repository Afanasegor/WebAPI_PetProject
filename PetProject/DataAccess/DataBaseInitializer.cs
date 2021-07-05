using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class DataBaseInitializer
    {
        public static void InitializeEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasData(
                new Student[]
                {
                    new Student("Egor", "Afanasyev", "89274690937") { Id = 1}, // { Id = 1, Name = "Egor", Surname = "Afanasyev" },
                    new Student("Bulat", "Zakirov", "81234690912") { Id = 2}, // { Id = 2, Name = "Bulat", Surname = "Zakirov" },
                    new Student("Mikhail", "Ibragimov", "83124800228") { Id = 3}, // { Id = 3, Name = "Mikhail", Surname = "Ibragimov" }
                    new Student { Id = 4, Name = "Damir", Surname = "Gilmeev", PhoneNumber = "89241567831", Email = "damir.gilmeev@epam.com"}
                });

            modelBuilder.Entity<Lecturer>()
                .HasData(
                new Lecturer[]
                {
                    new Lecturer("Ivan", "Ivanov") { Id = 1}, // { Id = 1, Name = "Ivan", Surname = "Ivanov", FullName = "Ivan Ivanov", Email = "ivan.ivanov.lecturer@epam.com"},
                    new Lecturer("John", "Doe") { Id = 2 }
                });

            modelBuilder.Entity<Lecture>()
                .HasData(
                new Lecture[]
                {
                    new Lecture {Id = 1, Name = "Algorithms", LecturerId = 1},
                    new Lecture {Id = 2, Name = "Data structures", LecturerId = 2},
                    new Lecture {Id = 3, Name = "Extensions", LecturerId = 1}
                });

        }
    }
}

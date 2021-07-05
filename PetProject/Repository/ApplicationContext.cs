using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new StudentMap(modelBuilder.Entity<Student>());
            new LecturerMap(modelBuilder.Entity<Lecturer>());
            new LectureMap(modelBuilder.Entity<Lecture>());
            new HomeworkMap(modelBuilder.Entity<Homework>());

            DataBaseInitializer.InitializeEntities(modelBuilder);
        }
    }
}

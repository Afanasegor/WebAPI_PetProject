using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class StudentMap
    {
        public StudentMap(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.HasKey(s => s.Id);
            entityBuilder
                .HasMany(s => s.Lectures)
                .WithMany(l => l.Students)
                .UsingEntity<Attendance>(
                    j => j
                    .HasOne(a => a.Lecture)
                    .WithMany(l => l.Attendances)
                    .HasForeignKey(a => a.LectureId),
                    j => j
                    .HasOne(a => a.Student)
                    .WithMany(s => s.Attendances)
                    .HasForeignKey(a => a.StudentId),
                    j =>
                    {
                        j.Property(a => a.AttendanceResult).HasDefaultValue(AttendanceConverter.GetAttendanceToBoolean(AttendanceType.absant));
                        j.HasKey(a => new { a.StudentId, a.LectureId });
                    }
                );
        }
    }
}

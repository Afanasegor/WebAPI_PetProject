using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class HomeworkMap
    {
        public HomeworkMap(EntityTypeBuilder<Homework> entityBuilder)
        {
            entityBuilder
                .Property(p => p.Mark).HasDefaultValue(0);
            entityBuilder
                .HasOne(h => h.Lecture)
                .WithMany(l => l.Homeworks)
                .HasForeignKey(h => h.LectureId);

            entityBuilder
                .HasOne(h => h.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(h => h.StudentId);
        }
    }
}

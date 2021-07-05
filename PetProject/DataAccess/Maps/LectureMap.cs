using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class LectureMap
    {
        public LectureMap(EntityTypeBuilder<Lecture> entityBuilder)
        {
            entityBuilder
                .HasOne(l => l.Lecturer)
                .WithMany(l => l.Lectures)
                .HasForeignKey(l => l.LecturerId);

            entityBuilder
                .Property(l => l.IsFinished).HasDefaultValue(false);
        }
    }
}

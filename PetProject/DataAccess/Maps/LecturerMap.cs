using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class LecturerMap
    {
        public LecturerMap(EntityTypeBuilder<Lecturer> entityBuilder)
        {
            entityBuilder.HasKey(l => l.Id);
        }
    }
}

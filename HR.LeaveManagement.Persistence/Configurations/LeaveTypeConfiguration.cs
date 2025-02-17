﻿using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = Guid.NewGuid(),
                Name = "Vacation",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            });

        // Here we can add database restrictions using code first approach
        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}

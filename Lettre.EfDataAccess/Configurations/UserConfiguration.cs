using Lettre.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.EfDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(u => u.Surname)
                .HasMaxLength(30);
            builder.Property(u => u.Password)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .IsRequired();

            builder.HasMany(u => u.Comments)
                .WithOne(com => com.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

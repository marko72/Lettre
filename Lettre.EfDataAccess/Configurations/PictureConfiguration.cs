using Lettre.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lettre.EfDataAccess.Configurations
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(pic => pic.Path)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(pic => pic.Post)
                .WithMany(p => p.Pictures)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pic => pic.Path)
                .IsUnique();

        }
    }
}

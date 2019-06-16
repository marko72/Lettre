using Lettre.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lettre.EfDataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.Content)
                .HasMaxLength(8000)
                .IsRequired();

            builder.HasMany(p => p.Pictures)
                .WithOne(pic => pic.Post)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(p => p.Comments)
                .WithOne(com => com.Post)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

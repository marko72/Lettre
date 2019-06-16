using Lettre.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lettre.EfDataAccess.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(com => com.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(com => com.Post)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

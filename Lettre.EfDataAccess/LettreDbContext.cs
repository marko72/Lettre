using Lettre.Domain;
using Lettre.EfDataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lettre.EfDataAccess
{
    public class LettreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles{ get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach(var entry in entries)
            {
                if (entry.Entity is Domain.BaseEntity item && entry.State == EntityState.Added)
                {
                    item.CreatedAt = DateTime.Now;
                    item.IsDeleted = false;
                }
            }

            return base.SaveChanges();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SHLH34O\SQLEXPRESS01;Initial Catalog=Lettre;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());
        }

        
    }
}

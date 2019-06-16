﻿// <auto-generated />
using System;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lettre.EfDataAccess.Migrations
{
    [DbContext(typeof(LettreDbContext))]
    [Migration("20190614143017_prvamigracija")]
    partial class prvamigracija
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lettre.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Lettre.Domain.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Lettre.Domain.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("Path")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Lettre.Domain.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(8000);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Lettre.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Lettre.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.Property<string>("Surname")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lettre.Domain.Comment", b =>
                {
                    b.HasOne("Lettre.Domain.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Lettre.Domain.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Lettre.Domain.Picture", b =>
                {
                    b.HasOne("Lettre.Domain.Post", "Post")
                        .WithMany("Pictures")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Lettre.Domain.Post", b =>
                {
                    b.HasOne("Lettre.Domain.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lettre.Domain.User", b =>
                {
                    b.HasOne("Lettre.Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
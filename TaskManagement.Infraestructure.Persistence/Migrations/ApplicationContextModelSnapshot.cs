﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Infraestructure.Persistence.Context;

#nullable disable

namespace TaskManagement.Infraestructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManagement.Core.Domain.Entities.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTaskStatus")
                        .HasColumnType("int");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("IdTaskStatus");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Core.Domain.Entities.TasksStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TaskStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2024, 7, 2, 19, 35, 13, 299, DateTimeKind.Local).AddTicks(2941),
                            IsDeleted = false,
                            ModifiedBy = "N/A",
                            ModifiedDate = new DateTime(2024, 7, 2, 19, 35, 13, 299, DateTimeKind.Local).AddTicks(2954),
                            Name = "New"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2024, 7, 2, 19, 35, 13, 299, DateTimeKind.Local).AddTicks(2957),
                            IsDeleted = false,
                            ModifiedBy = "N/A",
                            ModifiedDate = new DateTime(2024, 7, 2, 19, 35, 13, 299, DateTimeKind.Local).AddTicks(2957),
                            Name = "In Progress"
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2024, 7, 2, 19, 35, 13, 299, DateTimeKind.Local).AddTicks(2959),
                            IsDeleted = false,
                            ModifiedBy = "N/A",
                            ModifiedDate = new DateTime(2024, 7, 2, 19, 35, 13, 299, DateTimeKind.Local).AddTicks(2959),
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("TaskManagement.Core.Domain.Entities.Tasks", b =>
                {
                    b.HasOne("TaskManagement.Core.Domain.Entities.TasksStatus", "TaskStatus")
                        .WithMany("Tasks")
                        .HasForeignKey("IdTaskStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskStatus");
                });

            modelBuilder.Entity("TaskManagement.Core.Domain.Entities.TasksStatus", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}

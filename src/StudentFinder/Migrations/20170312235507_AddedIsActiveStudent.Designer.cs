using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentFinder.Data;

namespace StudentFinder.Migrations
{
    [DbContext(typeof(StudentFinderContext))]
    [Migration("20170312235507_AddedIsActiveStudent")]
    partial class AddedIsActiveStudent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentFinder.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("From")
                        .IsRequired();

                    b.Property<string>("Label")
                        .IsRequired();

                    b.Property<string>("To")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("StudentFinder.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Contact")
                        .IsRequired();

                    b.Property<string>("Domain")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("School");
                });

            modelBuilder.Entity("StudentFinder.Models.Space", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("Room")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Space");
                });

            modelBuilder.Entity("StudentFinder.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GradeLevel")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("StudentSchoolId")
                        .IsRequired();

                    b.Property<int>("StudentsSchool");

                    b.Property<string>("fName")
                        .IsRequired();

                    b.Property<string>("lName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("StudentFinder.Models.StudentScheduleSpace", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("ScheduleId");

                    b.Property<int>("SpaceId");

                    b.HasKey("StudentId", "ScheduleId", "SpaceId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("SpaceId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentScheduleSpace");
                });

            modelBuilder.Entity("StudentFinder.Models.StudentScheduleSpace", b =>
                {
                    b.HasOne("StudentFinder.Models.Schedule", "Schedule")
                        .WithMany("StudentScheduleSpace")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentFinder.Models.Space", "Space")
                        .WithMany("StudentScheduleSpace")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentFinder.Models.Student", "Student")
                        .WithMany("StudentScheduleSpace")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

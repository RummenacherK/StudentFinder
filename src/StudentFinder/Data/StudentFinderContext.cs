using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentFinder.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFinder.Data
{
    public class StudentFinderContext : DbContext
    {
        public StudentFinderContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentScheduleSpace>()
                .HasKey(s => new { s.StudentId, s.ScheduleId, s.SpaceId });
            
        }

        public StudentFinderContext(DbContextOptions<StudentFinderContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Space> Space { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentScheduleSpace> StudentScheduleSpace { get; set; }
        public DbSet<Level> Level { get; set; }
    }
}

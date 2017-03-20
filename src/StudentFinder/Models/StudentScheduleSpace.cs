using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.Models
{
    public class StudentScheduleSpace
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public int SpaceId { get; set; }
        public Space Space { get; set; }

        //public int SchoolId { get; set; }
        //public School School { get; set; }
    }
}

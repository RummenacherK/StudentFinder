using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentFinder.Models;

namespace StudentFinder.ViewModels
{
    public class StudentCreateViewModel : Student
    {
        public int ScheduleId { get; set; }
        public int SpaceId { get; set; }
        public int StudentId { get; set; }
    }
}

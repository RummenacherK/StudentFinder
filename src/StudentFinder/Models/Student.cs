using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StudentFinder.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "School")]
        public int StudentsSchool { get; set; }
        [Required]
        [Display(Name ="Student Id")]
        public string StudentSchoolId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string fName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string lName { get; set; }
        [Required]
        [Display(Name = "Grade")]
        public int GradeLevelId { get; set; }
        [Required]
        [Display(Name = "Current Student?")]
        public bool IsActive { get; set; }

        public ICollection<StudentScheduleSpace> StudentScheduleSpace { get; set; }
        public virtual Level Level { get; set; }

    }
}

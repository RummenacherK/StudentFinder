using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.Models
{
    public class Space
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Room { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int SchoolId { get; set; }

        public ICollection<StudentScheduleSpace> StudentScheduleSpace { get; set; }
        //public virtual Space Spaces { get; set; }

    }
}

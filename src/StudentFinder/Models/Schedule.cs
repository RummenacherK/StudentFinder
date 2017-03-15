using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.Models
{
    public class Schedule
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        public string Label { get; set; }
        
        
        public DateTime? From { get; set; }
       
        [Required]
        [RegularExpression(@"^([1-9]|0[1-9]|1[0-2]):[0-5][0-9](am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        public string FromValue
        {
            get
            {
                return From.HasValue ? From.Value.ToString("hh:mm tt") : string.Empty;
            }

            set
            {
                From = DateTime.Parse(value);
            }
        }
        
        public DateTime? To { get; set; }
        [Required]
        [RegularExpression(@"^([1-9]|0[1-9]|1[0-2]):[0-5][0-9](am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        public string ToValue
        {
            get
            {
                return To.HasValue ? To.Value.ToString("hh:mm tt") : string.Empty;
            }

            set
            {
                To = DateTime.Parse(value);
            }
        }

        public ICollection<StudentScheduleSpace> StudentScheduleSpace { get; set; }
        //public virtual Schedule Schedules { get; set; }
        
    }
}

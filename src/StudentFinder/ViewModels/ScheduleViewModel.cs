using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.ViewModels
{
    public class ScheduleViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]   
        public int From { get; set; }
        [Required]
        public int To { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan FromTime
        {
            get
            {
                return TimeSpan.FromMinutes((double)From);
            }
           
        }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan ToTime
        {
            get
            {
                return TimeSpan.FromMinutes((double)To);
            }
           
        }

       

    }
}

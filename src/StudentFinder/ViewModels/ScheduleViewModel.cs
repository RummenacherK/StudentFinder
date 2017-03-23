using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;

namespace StudentFinder.ViewModels
{
   // [Validator(typeof(ScheduleViewModelValidator))]
    public class ScheduleViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Label { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public int From { get; set; }
        [Required]
        [Display(Name = "End Time")]
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
    //public class ScheduleViewModelValidator : AbstractValidator<ScheduleViewModel>
    //{
    //    public ScheduleViewModelValidator()
    //    {
    //        RuleFor(s => s.To).GreaterThan(s => s.From);
    //    }
    //}
}

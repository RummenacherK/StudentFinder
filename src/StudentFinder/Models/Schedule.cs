﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;

namespace StudentFinder.Models
{
   // [Validator(typeof(ScheduleValidator))]
    public class Schedule
    {
        [Key]
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

        public ICollection<StudentScheduleSpace> StudentScheduleSpace { get; set; }        
    }

    //public class ScheduleValidator : AbstractValidator<Schedule>
    //{
    //    public ScheduleValidator()
    //    {
    //        RuleFor(s => s.To).GreaterThan(s => s.From);
    //    }
    //}
}

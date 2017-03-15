using System;
using StudentFinder.Models;
using StudentFinder.Controllers;
using StudentFinder.Data;
using StudentFinder.Infrastructure;
using StudentFinder.Services;
using StudentFinder.ViewModels;
using StudentFinder.Migrations;
using System.Linq;

namespace StudentFinder.Infrastructure
{
    public class Utilities
    {

        private readonly StudentFinderContext _context;
        public Utilities(StudentFinderContext context)
        {
            _context = context;
        }


       // public TimeSpan TimeOfDay { get; }

        public int GetPeriod(DateTime today)
        {
            Schedule schedule = new Schedule { };
            
            var hh = today.Hour;
            var mm = today.Minute;

            var schedule1 = _context.Schedule.Where(s => s.SchoolId == SchoolId);

                           
            where(s => s.fromH > hh ||)
            if ( >= Schedule.FromValue && DateTime.Now <= Schedule.ToValue)
            {
                return Schedule.Id;
            }
            else
            {
                return default;
            }
           }
        } 
    }
}

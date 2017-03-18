using System;
using StudentFinder.Models;
using StudentFinder.Controllers;
using StudentFinder.Data;
using StudentFinder.Infrastructure;
using StudentFinder.Services;
using StudentFinder.ViewModels;
using StudentFinder.Migrations;
using System.Linq;
using System.Collections.Generic;

namespace StudentFinder.Infrastructure
{
    public class Utilities
    {


        private StudentFinderContext _context;

        public Utilities(StudentFinderContext context)
        {
            _context = context;
        }

       
        // Get Current Time of Day and Convert Hours and Minutes to Int    

        // Get Schedule data and compare to current time


        public int CompareTimes(DateTime today, int schoolid)

        {
            
            int hours = today.Hour;
            int min = today.Minute;
            int total_min = (hours * 60) + min;
            var schedule = _context.Schedule.Where(s => s.SchoolId == schoolid);
            return schedule.Where(s => s.From >= total_min && s.To <= total_min).Select(s => s.Id).SingleOrDefault();


            
        }
    }
}         

    

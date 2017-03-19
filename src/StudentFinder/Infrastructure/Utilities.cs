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
    public static class Utilities
    {
    

        //private StudentFinderContext _context;

        //public Utilities(StudentFinderContext context)
        //{
        //    _context = context;
        //}

       
        // Get Current Time of Day and Convert Hours and Minutes to Int    

        // Get Schedule data and compare to current time

        public static int CompareTimes(DateTime today)
        {
            using (var db = new StudentFinderContext())
            {
                int Id = 0;
                int hours = today.Hour;
                int min = today.Minute;
                int total_min = (hours * 60) + min;
                var schedule = db.Schedule.Where(s => s.From == Id);

                return total_min;
            }
        }
    }
}       

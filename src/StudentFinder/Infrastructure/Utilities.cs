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

<<<<<<< HEAD

        private readonly StudentFinderContext _context;
        public Utilities(StudentFinderContext context)
        {
            _context = context;
        }
=======
>>>>>>> refs/remotes/origin/Development

        private readonly StudentFinderContext _context;

        public Utilities(StudentFinderContext context)
        {

        }

        // Get Current Time of Day and Convert Hours and Minutes to Int    

        // Get Schedule data and compare to current time

        public static int CompareTimes(DateTime today)
        {
            int Id = 0;
            int hours = today.Hour;
            int min = today.Minute;
            int total_min = (hours * 60) + min;
            var schedule = _context.Schedule.Where(s => s.From == Id);

<<<<<<< HEAD
          where(s => s.fromH > hh ||)
          if ( >= Schedule.FromValue && DateTime.Now <= Schedule.ToValue)
          {
              return Schedule.Id;
          }
          else
          {
              return default;
          }
          */
=======
            for (int x = 0; Id < 10; x++) { Console.WriteLine(total_min); }
        }
>>>>>>> refs/remotes/origin/Development
    }
}         

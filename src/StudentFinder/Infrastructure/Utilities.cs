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

        // Get Current Time of Day and Convert Hours and Minutes to Int

       /*
        
        public int GetPeriod()
        {
            var schedule = _context.Schedule.ToArray();
            var schedulehour =0;

          

            foreach(var item in schedule)
            {
                string[] test = schedule.
            }

            int currenthh = DateTime.Today.Hour;
            int currentmm = DateTime.Today.Minute;

        foreach (var item in schedule)
        {
            if(currenthh > item.To.)
        }


       //     var selectedTime = _context.Schedule.Where(s => s.From < currentTime )
                
          //      return selectedTime;
          //      }


        /*  
          // public TimeSpan TimeOfDay { get; }

      public int GetPeriod(DateTime today)
      {

          Schedule schedule = new Schedule();
          var tod = today.TimeOfDay;

          string tString = tod.ToString();


          var tSchedule = schedule.FromValue;

          if(tSchedule < tString)




         /*

          int result = DateTime.Compare(date1, date2);
          string relationship;

          if (result < 0)
              relationship = "is earlier than";
          else if (result == 0)
              relationship = "is the same time as";
          else
              relationship = "is later than";

          Console.WriteLine("{0} {1} {2}", date1, relationship, date2);


          /*
          var schedule = _context.Schedule.Where(s => s.FromValue == ;

          var hh = today.Hour;
          var mm = today.Minute;

          int result = DateTime.Compare(hh, schedule)


          var schedule1 = _context.Schedule.Where(s => s.SchoolId == SchoolId);
          var schedule2 = _context.Schedule.Where(s => s.ScheduleId == SchduleId);
          var schedule3 = _context.Schedule.Where(s => s.From >= hh && s.ToH < hh);

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
    }
    } 
   // }
//}

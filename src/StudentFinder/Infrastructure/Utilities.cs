using System;
using StudentFinder.Models;
using StudentFinder.Controllers;
using StudentFinder.Data;
using StudentFinder.Infrastructure;
using StudentFinder.Services;
using StudentFinder.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace StudentFinder.Infrastructure
{
    [Authorize]
    public class Utilities
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private StudentFinderContext _context;

        public Utilities(StudentFinderContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Utilities()
        { }
        // Get Current Time of Day and Convert Hours and Minutes to Int    

        // Get Schedule data and compare to current time

        public Tuple<int, string> CompareTimes(DateTime today, int schoolId)
        {
            string period_label = string.Empty;
            int hours = today.Hour;
            int min = today.Minute;
            int total_min = (hours * 60) + min;
            var schedule = _context.Schedule.Where(s => s.SchoolId == schoolId).Select(x => x).ToList();
            var period = schedule.Where(s => s.From >= total_min && s.To <= total_min).Select(s => s.Id).SingleOrDefault();
            // We found a period
            if (period > 0)
            {
                period_label = _context.Schedule.Where(x => x.Id == period).Select(x => x.Label).SingleOrDefault();

                return new Tuple<int, string>(period, period_label);               
            }
            else // No period found above, add 15 min
            {
                period_label = "Inbetween Periods, Next Period is ";

                total_min = total_min + 15;
                var period2 = schedule.Where(s => s.From >= total_min && s.To <= total_min).Select(s => s.Id).SingleOrDefault();

                if (period2 > 0)  // we have a period after adding 15 min
                {
                    period_label = period_label + _context.Schedule.Where(x => x.Id == period).Select(x => x.Label).SingleOrDefault();

                    return new Tuple<int, string>(period2, period_label);
                }
                else // no period found
                {
                    period_label = "No more periods for today.";

                    // end of day
                    return new Tuple<int, string>(-1, period_label);
                }
            }              
        }
    }
}         

    

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

        public int CompareTimes(DateTime today, int schoolId)
        {
            
                int hours = today.Hour;
                int min = today.Minute;
                int total_min = (hours * 60) + min;
                var schedule = _context.Schedule.Select(x => x).ToList();
                return schedule.Where(s => s.From >= total_min && s.To <= total_min && s.SchoolId == schoolId).Select(s => s.Id).SingleOrDefault();
                
        }

    }
}         

    

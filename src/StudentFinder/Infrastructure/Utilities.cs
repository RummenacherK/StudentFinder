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
            //using (var db = StudentFinderContext)
            //{
                int hours = today.Hour;
                int min = today.Minute;
                int total_min = (hours * 60) + min;
                var schedule = _context.Schedule.Where(s => s.SchoolId == schoolId).Select(x => x).ToList();
                return schedule.Where(s => s.From >= total_min && s.To <= total_min).Select(s => s.Id).SingleOrDefault();
            //}

        }

        //[Authorize(Roles = "User")]
        //public async Task<int> GetUserSchool(ClaimsPrincipal Id)
        //{
          
        //    var userClaim = _userManager.GetUserId(Id);
        //   // var userId = Id;
        //    var user = await _userManager.FindByIdAsync(userClaim);
        //    if (user != null)
        //    {
        //        var has_claim = false;
        //        var user_claim_list = await _userManager.GetClaimsAsync(user);
        //        if (user_claim_list.Count > 0)
        //        {
        //            has_claim = user_claim_list[0].Type == "SchoolId";

        //            var test = Convert.ToInt32(user_claim_list[0].Value);

        //            return test;
        //        }


        //        //if (!has_claim)
        //        //{
        //        //   await _userManager.AddClaimAsync(user, new Claim("SchoolId", user.SchoolId.ToString()));
        //        //}
        //    }

        //    //var userSchool = user.Claims;

        //    return 0;

        //}

    }
}         

    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentFinder.Data;
using StudentFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentFinder.ViewModels;
using StudentFinder.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace StudentFinder.Controllers
{
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext db_context;

        public ManageUsersController(ApplicationDbContext context)
        {
            db_context = context;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? page)
        {


            var Users = db_context.Users.Select(x => x);

            var selectedUsers = Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                lName = u.lName,
                fName = u.fName,
                SchoolId = u.SchoolId,
                Email = u.Email
                
            });

            int pageSize = 25;

            return View(await PaginatedList<UserViewModel>.CreateAsync(selectedUsers.AsNoTracking(), page ?? 1, pageSize));
        }

    }
}
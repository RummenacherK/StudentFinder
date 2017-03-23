using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentFinder.Data;
using StudentFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace StudentFinder.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class ManageRolesController : Controller
    {
        private ApplicationDbContext db_context;

        public ManageRolesController(ApplicationDbContext context)
        {
            db_context = context;
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var Roles = db_context.Roles.ToList();
            return View(Roles);
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            // Set Normalized value
            Role.NormalizedName = Role.Name.ToUpper();
            db_context.Roles.Add(Role);
            db_context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentFinder.Data;
using StudentFinder.Models;
using StudentFinder.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace StudentFinder.Controllers
{
    public class SpacesController : Controller
    {
        private readonly StudentFinderContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SpacesController(StudentFinderContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        // GET: Spaces
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,
            string searchString, int? page)
        {
            //We need to get the ID of the user's school before we can show the specific schedule for them
            //var user = _userManager.GetUserId(test); /*.GetUserAsync(test);*/
            int? schoolId = _session.GetInt32("schoolId").Value;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["RoomSortParm"] = String.IsNullOrEmpty(sortOrder) ? "room_desc" : "";
            ViewData["LocationSortParm"] = sortOrder == "location" ? "location_desc" : "Location";
            ViewData["DescriptionSortParm"] = sortOrder == "description" ? "description_desc" : "Description";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var space = from s in _context.Space
                        where s.SchoolId == schoolId
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                space = space.Where(s => s.Room.Contains(searchString) || s.Description.Contains(searchString) || s.Location.Contains(searchString));
                //|| s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "room_desc":
                    space = space.OrderByDescending(s => s.Room);
                    break;
                case "location":
                    space = space.OrderBy(s => s.Location);
                    break;

                case "location_desc":
                    space = space.OrderByDescending(s => s.Location);
                    break;
                case "description":
                    space = space.OrderBy(s => s.Description);
                    break;

                case "description_desc":
                    space = space.OrderByDescending(s => s.Description);
                    break;
                default:
                    space = space.OrderBy(s => s.Room);
                    break;

            }

            int pageSize = 10;
            return View(await PaginatedList<Space>.CreateAsync(space.AsNoTracking(), page ?? 1, pageSize));


        }


        // GET: Spaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //We need to get the ID of the user's school before we can show the specific schedule for them
            //var user = _userManager.GetUserId(test); /*.GetUserAsync(test);*/
            int schoolId = _session.GetInt32("schoolId").Value;


            var space = await _context.Space.Where(s => s.SchoolId == schoolId).SingleOrDefaultAsync(m => m.Id == id);
            if (space == null)
            {
                return NotFound();
            }

            return View(space);
        }

        // GET: Spaces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Location,Room,SchoolId")] Space space)
        {
            int schoolId = _session.GetInt32("schoolId").Value;
            space.SchoolId = schoolId;

            if (ModelState.IsValid)
            {
                //We need to get the ID of the user's school before we can show the specific schedule for them
                //var user = _userManager.GetUserId(test); /*.GetUserAsync(test);*/
                

                _context.Add(space);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(space);
        }

        // GET: Spaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //We need to get the ID of the user's school before we can show the specific schedule for them
            //var user = _userManager.GetUserId(test); /*.GetUserAsync(test);*/
            int schoolId = _session.GetInt32("schoolId").Value;

            var space = await _context.Space.Where(s => s.SchoolId == schoolId).SingleOrDefaultAsync(m => m.Id == id);
            if (space == null)
            {
                return NotFound();
            }
            return View(space);
        }

        // POST: Spaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Location,Room")] Space space)
        {
            if (id != space.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(space);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaceExists(space.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(space);
        }

        // GET: Spaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var space = await _context.Space.SingleOrDefaultAsync(m => m.Id == id);
            if (space == null)
            {
                return NotFound();
            }

            return View(space);
        }

        // POST: Spaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var space = await _context.Space.SingleOrDefaultAsync(m => m.Id == id);
            _context.Space.Remove(space);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SpaceExists(int id)
        {
            return _context.Space.Any(e => e.Id == id);
        }

        //[Authorize(Roles = "User")]
        //public async Task<int> GetUserSchool()
        //{
        //    //Get School of the User:  See method at bottom of controller

        //    var test = HttpContext.User;

        //    if (test == null)
        //    {
        //        RedirectToRoute("Students", "Home");
        //    }
        //    var userClaim = _userManager.GetUserId(test);
        //    // var userId = Id;
        //    var user = await _userManager.FindByIdAsync(userClaim);
        //    if (user == null) return 0;
        //    var has_claim = false;
        //    var user_claim_list = await _userManager.GetClaimsAsync(user);
        //    if (user_claim_list.Count > 0)
        //    {
        //        //has_claim = user_claim_list[0].Type == "SchoolId";

        //        var newUserSchool = Convert.ToInt32(user_claim_list[2].Value);

        //        return newUserSchool;
        //    }

        //    return 0;
        //}

    }
}
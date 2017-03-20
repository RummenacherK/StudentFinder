using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentFinder.Data;
using StudentFinder.Models;
using StudentFinder.ViewModels;
using StudentFinder.Infrastructure;
using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Cli.Utils;
using NuGet.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace StudentFinder.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly StudentFinderContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public StudentsController(StudentFinderContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Home()
        {
            int schoolId = await GetUserSchool();

            HttpContext.Session.SetInt32("schoolId", schoolId);

            return View();
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchString, int? page, int spaceListFilter = 0)
        {
            Utilities util = new Utilities();

            //Get School of the User:  See method at bottom of controller
            int schoolId = await GetUserSchool();
                        
            HttpContext.Session.SetInt32("schoolId", schoolId);
            //We need to get the ID of the user's school before we can show the specific schedule for them
            //var user = _userManager.GetUserId(test); /*.GetUserAsync(test);*/



            //int schoolId = _session.GetInt32("schoolId").Value;
                  
            
            //Create Viewbags for the following data
            var spaceList = _context.Space.Where(s => s.SchoolId == schoolId).OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            var scheduleList = _context.Schedule.Where(s => s.SchoolId == schoolId).OrderBy(s => s.Label).Select(a => new { id = a.Id, value = a.From, value2 = a.To }).ToList();
            ViewBag.ScheduleSelectList = new SelectList(scheduleList, "id", "value", "value2");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");
           
            ViewBag.searchString = searchString;
           
            //Get Today and the schedule for today
            var today = DateTime.Now;
           
            var currentPeriod = util.CompareTimes(today, schoolId);
           
            //Create Viewbag of current period
            ViewBag.DisplayPeriod = _context.Schedule.Where(x => x.Id == currentPeriod).Select(x => x.Label).SingleOrDefault();

            //Select only Active Students & students from that school       
            var activeStudents = _context.StudentScheduleSpace.Where(a => a.Student.IsActive == true && a.Student.StudentsSchool == schoolId).Select(x => x);
                        
            //Select entry on SSS table which matches the current time Period
            var s_all = activeStudents.Where(s => s.ScheduleId == currentPeriod).Select(x => x);
                                
            if (spaceListFilter > 0)
            {
                s_all = s_all.Where(s => s.SpaceId == spaceListFilter);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                s_all = s_all.Where(s => s.Student.fName.Contains(searchString) || s.Student.lName.Contains(searchString));
            }
                   
            var selectedStudents = s_all.Select(s => new StudentsViewModel()
            {
                StudentId = s.Student.Id,
                StudentsSchool = s.Student.StudentsSchool,
                StudentSchoolId = s.Student.StudentSchoolId,
                fName = s.Student.fName,
                lName = s.Student.lName,
                LevelId = s.Student.LevelId,
                IsActive = s.Student.IsActive,
                SpaceId = s.Space.Id,
                Room = s.Space.Room,
                Location = s.Space.Location,
                GradeLevel = s.Student.Level.GradeLevel

            });

            int pageSize = 25;

            return View(await PaginatedList<StudentsViewModel>.CreateAsync(selectedStudents.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int schoolId = _session.GetInt32("schoolId").Value;

            var spaceList = _context.Space.Where(s => s.SchoolId == schoolId).OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            var scheduleList = _context.Schedule.Where(s => s.SchoolId == schoolId).OrderBy(s => s.Label).Select(a => new { id = a.Id, value = a.From, value2 = a.To }).ToList();
            ViewBag.ScheduleSelectList = new SelectList(scheduleList, "id", "value", "value2");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");

            var student = await _context.Student.Where(s => s.StudentsSchool == schoolId).SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {

            int? schoolId = HttpContext.Session.GetInt32("schoolId");
            if (schoolId == null)
            {
                return View("Home");
            }


            var spaceList = _context.Space.Where(s => s.SchoolId == schoolId).OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");
            
            IEnumerable<Schedule> scheduleList = _context.Schedule.Where(s => s.SchoolId == schoolId).OrderBy(x => x.From).ToList();
            ViewBag.scheduleViewBag = scheduleList;

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,LevelId,StudentSchoolId,StudentsSchool,fName,lName,IsActive")] Student student, 
            int[] scheduleIdList, 
            params int [] spaceIdList)
        {
            //Add error handling for scheduleId and SpaceId


            int? schoolId = HttpContext.Session.GetInt32("schoolId");
            if (schoolId == null)
            {
                return View("Home");
            }
            else
            {
                student.StudentsSchool = schoolId.Value;
            }


            if (ModelState.IsValid)
            {


                _context.Add(student);
                await _context.SaveChangesAsync();
                
                int studentId = student.Id;

                SetStudentSchedule(studentId, scheduleIdList, spaceIdList);
                
                return RedirectToAction("Index");
            }

            //add data back to view so if something goes wrong user doesnt have to reenter it
            return View();
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
           var studentId = id;

            int schoolId = _session.GetInt32("schoolId").Value;


            if (schoolId == 0)
            {
                return View("Home");
            }
           
          
            
            //add check method here for correct school/claims bool
           

            if (studentId == 0)
            {
                return NotFound();
            }

            IEnumerable<StudentScheduleSpace> studentSchedule = GetStudentSchedule(studentId).ToList();

            ViewBag.StudentScheduleList = studentSchedule;

            IEnumerable<Schedule> scheduleList = _context.Schedule.Where(s => s.SchoolId == schoolId).OrderBy(x => x.From).ToList();
            ViewBag.scheduleViewBag = scheduleList;

            var spaceList = _context.Space.Where(s => s.SchoolId == schoolId).OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");           

            var schoolList = _context.School.Select(s => new { id = s.Id, value = s.Name }).ToList();
            ViewBag.schoolSelectList = new SelectList(schoolList, "id", "value");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value", GetStudentLevel(studentId).Item1);

            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }
            
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,LevelId,StudentSchoolId,StudentsSchool,fName,lName,IsActive")] Student student,
            int[] scheduleIdList,
            params int[] spaceIdList) 
        {

            int schoolId = _session.GetInt32("schoolId").Value;
            
            var editStudent = _context.Student.Where(s => s.StudentsSchool == schoolId).Select(x => x.Id == student.Id);
            if (!editStudent.Any())
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();

                    int studentId = student.Id;

                    SetStudentSchedule(studentId, scheduleIdList, spaceIdList);
                    return RedirectToAction("Index", "Students");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int schoolId = _session.GetInt32("schoolId").Value;

            var student = await _context.Student.Where(s => s.StudentsSchool == schoolId).SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int schoolId = _session.GetInt32("schoolId").Value;

            var student = await _context.Student.Where(s => s.StudentsSchool == schoolId).SingleOrDefaultAsync(m => m.Id == id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("EditSchedule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(int newScheduleId, int newStudentId, int newSpaceId)
        {
            StudentScheduleSpace newSchedule = new StudentScheduleSpace();
            newSchedule.ScheduleId = newScheduleId;
            newSchedule.StudentId = newStudentId;
            newSchedule.SpaceId = newSpaceId;

            _context.StudentScheduleSpace.Add(newSchedule);
            await _context.SaveChangesAsync();
                            
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }     

        public List<StudentScheduleSpace> GetStudentSchedule(int Id)
        {
            var studentSchedule = _context.StudentScheduleSpace.Where(x => x.StudentId == Id).Select(x => x).ToList();

            return studentSchedule;
        }
                
        public async void SetStudentSchedule(int studentId, int[] scheduleIdList, int[] spaceIdList)
        {
           
            var student_schedule = GetStudentSchedule(studentId);

            if (student_schedule.Any())
            {

                var deleteEntry =
                    from row in _context.StudentScheduleSpace
                    where row.StudentId == studentId
                    select row;

                foreach (var row in deleteEntry)
                {
                    _context.StudentScheduleSpace.Remove(row);
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                int i = 0;
                foreach (var item in scheduleIdList)
                {
                    _context.StudentScheduleSpace.Add(
                       new StudentScheduleSpace
                       {
                           StudentId = studentId,
                           ScheduleId = scheduleIdList[i],
                           SpaceId = spaceIdList[i]
                       });
                    i++;
                }

                Task saveSchedule = _context.SaveChangesAsync();
                await saveSchedule;
                return;
            }
        }

        public Tuple<int, string> GetStudentLevel(int studentId)
        {
            return new Tuple<int, string>(_context.Student.Where(x => x.Id == studentId).Select(x => x.LevelId).SingleOrDefault(),
                _context.Student.Where(x => x.Id == studentId).Select(x => x.Level.GradeLevel).SingleOrDefault());            
        }

        [Authorize(Roles = "User")]
        public async Task<int> GetUserSchool()
        {
            var test = HttpContext.User;

            if (test == null)
            {
                RedirectToRoute("Students", "Home");
            }

            var userClaim = _userManager.GetUserId(test);
            // var userId = Id;
            var user = await _userManager.FindByIdAsync(userClaim);
            if (user == null) return 0;
            var has_claim = false;
            var user_claim_list = await _userManager.GetClaimsAsync(user);
            if (user_claim_list.Count > 0)
            {
                //has_claim = user_claim_list[0].Type == "SchoolId";

                var newUserSchool = Convert.ToInt32(user_claim_list[2].Value);

                return newUserSchool;
            }

            return 0;
        }
    }


    //public void CompleteStudentSearch ()
    //{
    //    var students_all = _context.StudentScheduleSpace.Select(s => new StudentsViewModel()
    //    {
    //        StudentId = s.Student.Id,
    //        fName = s.Student.fName,
    //        lName = s.Student.lName,
    //        LevelId = s.Student.LevelId,
    //        SpaceId = s.Space.Id,
    //        Room = s.Space.Room,
    //        Location = s.Space.Location
    //    });

    //}



    // int i = 0;
    // foreach (var item in scheduleIdList)
    // {
    //   var selectedEntry = _context.StudentScheduleSpace.Where(s => s.StudentId == studentId && s.ScheduleId == scheduleIdList[i]).Select(s => s.Id);

    //     _context.StudentScheduleSpace.Remove(selectedEntry);


    //     _context.StudentScheduleSpace.Where(s => s.StudentId == studentId && s.ScheduleId == scheduleIdList[i]) Update(
    //         new StudentScheduleSpace
    //         {
    //             StudentId = studentId,
    //             ScheduleId = scheduleIdList[i],
    //             SpaceId = spaceIdList[i]
    //         }
    //         );
    //     i++;
    // }

    //await _context.SaveChangesAsync();

    // return;
}

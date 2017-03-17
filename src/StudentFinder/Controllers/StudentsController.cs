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
using Microsoft.DotNet.Cli.Utils;

namespace StudentFinder.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentFinderContext _context;

        public StudentsController(StudentFinderContext context)
        {
            _context = context;    
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchString, int? page, int spaceListFilter = 0 /*int some_ID = 0*/)
        {

            //var spaceSort = _context.StudentScheduleSpace.OrderBy(c => c. Space.Id).Select(a => new { id = a.i})

            var spaceList = _context.Space.OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            var scheduleList = _context.Schedule.OrderBy(s => s.Label).Select(a => new { id = a.Id, value = a.From, value2 = a.To }).ToList();
            ViewBag.ScheduleSelectList = new SelectList(scheduleList, "id", "value", "value2");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");
           
            ViewBag.searchString = searchString;


            //IQueryable<StudentsViewModel> studentsVM;
            
            var student = new Student();
                                       
            var some_ID = 20;

            //Select only Active Students       
            var activeStudents = _context.StudentScheduleSpace.Where(a => a.Student.IsActive == true).Select(x => x);

            //Select only students that have the current schedule Id
            var s_all = activeStudents.Where(s => s.ScheduleId == some_ID).Select(x => x);


            //Old s_all code
            //var s_all = _context.StudentScheduleSpace.Where(s => s.ScheduleId == some_ID).Select(x => x);



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
                fName = s.Student.fName,
                lName = s.Student.lName,
                GradeLevelId = s.Student.Level.Id,
                SpaceId = s.Space.Id,
                Room = s.Space.Room,
                Location = s.Space.Location,
                StudentSchoolId = s.Student.StudentSchoolId,
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

            var spaceList = _context.Space.OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            var scheduleList = _context.Schedule.OrderBy(s => s.Label).Select(a => new { id = a.Id, value = a.From, value2 = a.To }).ToList();
            ViewBag.ScheduleSelectList = new SelectList(scheduleList, "id", "value", "value2");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");

            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var spaceList = _context.Space.OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            IEnumerable<Schedule> scheduleList = _context.Schedule.OrderBy(x => x.From).ToList();
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
            [Bind("Id,GradeLevelId,StudentSchoolId,StudentsSchool,fName,lName,IsActive")] Student student, 
            int[] scheduleIdList, 
            params int [] spaceIdList)
        {
            //Add error handling for scheduleId and SpaceId

            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                
                int studentId = student.Id;

                SetStudentSchedule(studentId, scheduleIdList, spaceIdList);
                
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var spaceList = _context.Space.OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            IEnumerable<Schedule> scheduleList = _context.Schedule.OrderBy(x => x.From).ToList();
            ViewBag.scheduleViewBag = scheduleList;

            var schoolList = _context.School.Select(s => new { id = s.Id, value = s.Name }).ToList();
            ViewBag.schoolSelectList = new SelectList(schoolList, "id", "value");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");

            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,GradeLevel,StudentId,StudentsSchool,fName,lName")] Student student) 
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction("Index");
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

            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
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
            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
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

        public void CompleteStudentSearch ()
        {
            var students_all = _context.StudentScheduleSpace.Select(s => new StudentsViewModel()
            {
                StudentId = s.Student.Id,
                fName = s.Student.fName,
                lName = s.Student.lName,
                GradeLevelId = s.Student.GradeLevelId,
                SpaceId = s.Space.Id,
                Room = s.Space.Room,
                Location = s.Space.Location
            });

        }

        public List<StudentScheduleSpace> GetStudentSchedule(int Id)
        {
            var studentSchedule = _context.StudentScheduleSpace.Where(x => x.StudentId == Id).Select(x => x).ToList();

            return studentSchedule;
        }

        public void SetStudentSchedule(int studentId, int[] scheduleIdList, int[] spaceIdList)
        {
           
            var student_schedule = GetStudentSchedule(studentId);

            if (student_schedule.Any())
            {


                foreach (var row in student_schedule)
                {
                    _context.StudentScheduleSpace.Remove(row);
                }

                _context.SaveChangesAsync();
            }

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

            _context.SaveChangesAsync();
            
        }

    }
}

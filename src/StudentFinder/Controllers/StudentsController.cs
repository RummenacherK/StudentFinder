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
        public async Task<IActionResult> Index(string searchString, int? page, int spaceListFilter = 0, int schoolId = 1)
        {

            //We need to get the ID of the user's school before we can show the specific schedule for them


            var spaceList = _context.Space.OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
            ViewBag.SpaceSelectList = new SelectList(spaceList, "id", "value");

            var scheduleList = _context.Schedule.OrderBy(s => s.Label).Select(a => new { id = a.Id, value = a.From, value2 = a.To }).ToList();
            ViewBag.ScheduleSelectList = new SelectList(scheduleList, "id", "value", "value2");

            var gradeList = _context.Level.OrderBy(s => s.Id).Select(g => new { id = g.Id, value = g.GradeLevel }).ToList();
            ViewBag.gradeLevelSelectList = new SelectList(gradeList, "id", "value");
           
            ViewBag.searchString = searchString;



            //ANDREW:  PUT YOUR CODE HERE!
            //IQueryable<StudentsViewModel> studentsVM;

            //var student = new Student();
            //var today = DateTime.Now;
            //var currentPeriod = Utilities.CompareTimes(today);
            var currentPeriod = 20; //This will need to be updated from Andrew's code

            //END:  ANDREW SECTION

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
            [Bind("Id,LevelId,StudentSchoolId,StudentsSchool,fName,lName,IsActive")] Student student, 
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

            //add data back to view so if something goes wrong user doesnt have to reenter it
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
           var studentId = id;

            //add check method here for correct school/claims bool
            if (studentId == 0)
            {
                return NotFound();
            }

            IEnumerable<StudentScheduleSpace> studentSchedule = GetStudentSchedule(studentId).ToList();

            ViewBag.StudentScheduleList = studentSchedule;

            IEnumerable<Schedule> scheduleList = _context.Schedule.OrderBy(x => x.From).ToList();
            ViewBag.scheduleViewBag = scheduleList;

            var spaceList = _context.Space.OrderBy(s => s.Room).Select(a => new { id = a.Id, value = a.Room }).ToList();
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
            var editStudent = _context.Student.Select(x => x.Id == student.Id);
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
                LevelId = s.Student.LevelId,
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

        
        public async void SetStudentSchedule(int studentId, int[] scheduleIdList, int[] spaceIdList)
        {
           
            var student_schedule = GetStudentSchedule(studentId);

            //int i = 0;
            //foreach (var item in student_schedule)
            //{
            //    var test = _context.StudentScheduleSpace.Where(x => x.StudentId == studentId && x.ScheduleId == item.ScheduleId && x.SpaceId == item.SpaceId).Single();
                
            //    test.ForEachAsync(a =>
            //        {
            //            a.SpaceId = spaceIdList[i];
            //        });


            //    //await _context.SaveChangesAsync();
            //    i++;
            //}

            if (student_schedule.Any())
            {
                                
                    int i = 0;
                    foreach (var item in scheduleIdList)
                    {
                      var test = _context.StudentScheduleSpace.Where(x => x.StudentId == studentId && x.ScheduleId == item);
                    
                    test.ForEachAsync(a =>
                    {
                        a.SpaceId = spaceIdList[i];
                    });

                    
                    //await _context.SaveChangesAsync();
                     i++;
                    }

                _context.SaveChanges();


                return;

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


    }
}

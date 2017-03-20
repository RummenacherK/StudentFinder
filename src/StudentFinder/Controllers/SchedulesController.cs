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
    public class SchedulesController : Controller
    {
        private readonly StudentFinderContext _context;

        public SchedulesController(StudentFinderContext context)
        {
            _context = context;    
        }



        // Get Schedule
        public IActionResult Index()
        {

            var schedule = _context.Schedule.Select(x => x).ToList();

            var scheduleVM = schedule.Select(s => new ScheduleViewModel()
            {

                SchoolId = s.SchoolId,
                Label = s.Label,
                Id = s.Id,
                From = s.From,
                To = s.To
            });

            return View(scheduleVM);
        }

        // GET  Period Details
        public IActionResult Details()
        {



            var schedule = _context.Schedule.Select(x => x).ToList();

            var scheduleVM = schedule.Select(s => new ScheduleViewModel()
            {
                Label = s.Label,
                From = s.From,
                To = s.To
            });

            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET Create Period
        public IActionResult Create()
        {
          

            ViewBag.HourSelectList = ScheduleDropDown.ChooseHour();

            ViewBag.MinuteSelectList = ScheduleDropDown.ChooseMinute();

            return View();
        }

        // POST Create Period
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SchoolId,Label,From,To")] Schedule schedule, int fromMinute, int toMinute, int fromHour, int toHour)
        {
            schedule.From = (fromHour * 60) + fromMinute;

            schedule.To = (toHour * 60) + toMinute;

             if (ModelState.IsValid)
             {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
             }
             return View(schedule);
        }

        // GET Edit Period
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.SingleOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST Edit Period
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SchoolId,Label,From,To")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            return View(schedule);
        }

        // GET Delete Period
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.SingleOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Delete Period
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.SingleOrDefaultAsync(m => m.Id == id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}

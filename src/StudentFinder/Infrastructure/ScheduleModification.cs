using StudentFinder.Data;
using StudentFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentFinder.Controllers;

namespace StudentFinder.Infrastructure
{
    public static class ScheduleModification
    {

        public static List<StudentScheduleSpace> GetStudentSchedule(int Id)
        {
            using (var db = new StudentFinderContext())
            {

                var studentSchedule = db.StudentScheduleSpace.Where(x => x.StudentId == Id).Select(x => x).ToList();

                return studentSchedule;
            }
        }
        public async static void RemoveStudent(int studentId)
        {
            
            var student_schedule = GetStudentSchedule(studentId);
            using (var db = new StudentFinderContext())
            {

                foreach (var row in student_schedule)
                {
                    db.StudentScheduleSpace.Remove(row);
                }

                await db.SaveChangesAsync();
            }
        }
    }
}

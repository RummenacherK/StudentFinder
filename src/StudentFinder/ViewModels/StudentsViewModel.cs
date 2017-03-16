using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.ViewModels
{
    public class StudentsViewModel
    {

        //Student Info
        public int StudentId { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public int GradeLevelId { get; set; }
        public string StudentSchoolId { get; set; }
        public bool IsActive { get; set; }

        //School global Identifier
        public int StudentsSchool { get; set; }
        
        //Schedule Id
        public int ScheduleId { get; set; }

        //Space Info
        public int SpaceId { get; set; }
        public string Room { get; set; }
        public string Location { get; set; }
                
    }

   
}

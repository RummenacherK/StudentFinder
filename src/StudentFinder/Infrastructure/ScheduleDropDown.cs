using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.Infrastructure
{
    public static class ScheduleDropDown
    {
        public static IList<SelectListItem> ChooseHour()
        {


            IList<SelectListItem> _result = new List<SelectListItem>();

            _result.Add(new SelectListItem { Text = "00", Value = "0", });
            _result.Add(new SelectListItem { Text = "01", Value = "1", });
            _result.Add(new SelectListItem { Text = "02", Value = "2", });
            _result.Add(new SelectListItem { Text = "03", Value = "3", });
            _result.Add(new SelectListItem { Text = "04", Value = "4", });
            _result.Add(new SelectListItem { Text = "05", Value = "5", });
            _result.Add(new SelectListItem { Text = "06", Value = "6", });
            _result.Add(new SelectListItem { Text = "07", Value = "7", });
            _result.Add(new SelectListItem { Text = "08", Value = "8", });
            _result.Add(new SelectListItem { Text = "09", Value = "9", });
            _result.Add(new SelectListItem { Text = "10", Value = "10", });
            _result.Add(new SelectListItem { Text = "11", Value = "11", });
            _result.Add(new SelectListItem { Text = "12", Value = "12", });
            _result.Add(new SelectListItem { Text = "13", Value = "13", });
            _result.Add(new SelectListItem { Text = "14", Value = "14", });
            _result.Add(new SelectListItem { Text = "15", Value = "15", });
            _result.Add(new SelectListItem { Text = "16", Value = "16", });
            _result.Add(new SelectListItem { Text = "17", Value = "17", });
            _result.Add(new SelectListItem { Text = "18", Value = "18", });
            _result.Add(new SelectListItem { Text = "19", Value = "19", });
            _result.Add(new SelectListItem { Text = "20", Value = "20", });
            _result.Add(new SelectListItem { Text = "21", Value = "21", });
            _result.Add(new SelectListItem { Text = "22", Value = "22", });
            _result.Add(new SelectListItem { Text = "23", Value = "23", });
            
           

            return _result;
        }

        public static IList<SelectListItem> ChooseMinute()
        {


            IList<SelectListItem> _result = new List<SelectListItem>();

            _result.Add(new SelectListItem { Text = "00", Value = "0", });
            _result.Add(new SelectListItem { Text = "01", Value = "1", });
            _result.Add(new SelectListItem { Text = "02", Value = "2", });
            _result.Add(new SelectListItem { Text = "03", Value = "3", });
            _result.Add(new SelectListItem { Text = "04", Value = "4", });
            _result.Add(new SelectListItem { Text = "05", Value = "5", });
            _result.Add(new SelectListItem { Text = "06", Value = "6", });
            _result.Add(new SelectListItem { Text = "07", Value = "7", });
            _result.Add(new SelectListItem { Text = "08", Value = "8", });
            _result.Add(new SelectListItem { Text = "09", Value = "9", });
            _result.Add(new SelectListItem { Text = "10", Value = "10", });
            _result.Add(new SelectListItem { Text = "11", Value = "11", });
            _result.Add(new SelectListItem { Text = "12", Value = "12", });
            _result.Add(new SelectListItem { Text = "13", Value = "13", });
            _result.Add(new SelectListItem { Text = "14", Value = "14", });
            _result.Add(new SelectListItem { Text = "15", Value = "15", });
            _result.Add(new SelectListItem { Text = "16", Value = "16", });
            _result.Add(new SelectListItem { Text = "17", Value = "17", });
            _result.Add(new SelectListItem { Text = "18", Value = "18", });
            _result.Add(new SelectListItem { Text = "19", Value = "19", });
            _result.Add(new SelectListItem { Text = "20", Value = "20", });
            _result.Add(new SelectListItem { Text = "21", Value = "21", });
            _result.Add(new SelectListItem { Text = "22", Value = "22", });
            _result.Add(new SelectListItem { Text = "23", Value = "23", });
            _result.Add(new SelectListItem { Text = "24", Value = "24", });
            _result.Add(new SelectListItem { Text = "25", Value = "25", });
            _result.Add(new SelectListItem { Text = "26", Value = "26", });
            _result.Add(new SelectListItem { Text = "27", Value = "27", });
            _result.Add(new SelectListItem { Text = "28", Value = "28", });
            _result.Add(new SelectListItem { Text = "29", Value = "29", });
            _result.Add(new SelectListItem { Text = "30", Value = "30", });
            _result.Add(new SelectListItem { Text = "31", Value = "31", });
            _result.Add(new SelectListItem { Text = "32", Value = "32", });
            _result.Add(new SelectListItem { Text = "33", Value = "33", });
            _result.Add(new SelectListItem { Text = "34", Value = "34", });
            _result.Add(new SelectListItem { Text = "35", Value = "35", });
            _result.Add(new SelectListItem { Text = "36", Value = "36", });
            _result.Add(new SelectListItem { Text = "37", Value = "37", });
            _result.Add(new SelectListItem { Text = "38", Value = "38", });
            _result.Add(new SelectListItem { Text = "39", Value = "39", });
            _result.Add(new SelectListItem { Text = "40", Value = "40", });
            _result.Add(new SelectListItem { Text = "41", Value = "41", });
            _result.Add(new SelectListItem { Text = "42", Value = "42", });
            _result.Add(new SelectListItem { Text = "43", Value = "43", });
            _result.Add(new SelectListItem { Text = "44", Value = "44", });
            _result.Add(new SelectListItem { Text = "45", Value = "45", });
            _result.Add(new SelectListItem { Text = "46", Value = "46", });
            _result.Add(new SelectListItem { Text = "47", Value = "47", });
            _result.Add(new SelectListItem { Text = "48", Value = "48", });
            _result.Add(new SelectListItem { Text = "49", Value = "49", });
            _result.Add(new SelectListItem { Text = "50", Value = "50", });
            _result.Add(new SelectListItem { Text = "51", Value = "51", });
            _result.Add(new SelectListItem { Text = "52", Value = "52", });
            _result.Add(new SelectListItem { Text = "53", Value = "53", });
            _result.Add(new SelectListItem { Text = "54", Value = "54", });
            _result.Add(new SelectListItem { Text = "55", Value = "55", });
            _result.Add(new SelectListItem { Text = "56", Value = "56", });
            _result.Add(new SelectListItem { Text = "57", Value = "57", });
            _result.Add(new SelectListItem { Text = "58", Value = "58", });
            _result.Add(new SelectListItem { Text = "59", Value = "59", });
         

            return _result;
        }
    }
}

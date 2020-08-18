using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shifts.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public string Name { get; set; }
        public int Day { get; set; }
        public string Shift1{ get; set; }
        public string Remarks { get; set; }
        public Shift()
        {

        }
        public Shift(int id,  int day, string remarks, string shift, string name)
        {
            ShiftID = id;
            Day = day;
            Remarks = remarks;
            Shift1 = shift;
            Name = name;
        }
        public override string ToString()
        {
            return $"{ ShiftID},{Day },{Remarks },{Shift1 },{Name}";
        }
    }
}
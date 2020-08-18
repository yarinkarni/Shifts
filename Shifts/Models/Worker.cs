using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shifts.Models
{
    public class Worker
    {
        public int WorkerID { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string WorkerType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BirthdayDate { get; set; }
        public string Password { get; set; }
        public string PermissionType { get; set; }


        public Worker()
        {

        }
        public Worker(int WorkerID1, string Name1, string FamilyName1, string WorkerType1, string Email1,
            string PhoneNumber1, string Address1, string BirthdayDate1, string Password1, string PermissionType1)
        {
            WorkerID = WorkerID1;
            Name = Name1;
            FamilyName = FamilyName1;
            WorkerType = WorkerType1;
            Email = Email1;
            PhoneNumber = PhoneNumber1;
            Address = Address1;
            BirthdayDate = BirthdayDate1;
            Password = Password1;
            PermissionType = PermissionType1;

        }
        public override string ToString()
        {
            return $"{WorkerID}, {Name}, {FamilyName}, {WorkerType}, {Email},{PhoneNumber},{Address},{BirthdayDate}," +
                $"{Password},{PermissionType}";
        }
    }
}
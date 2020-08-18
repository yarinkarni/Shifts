using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shifts.Models
{
    public class WorkerDB
    {
        static bool local = false;
        static string strCon = null;
        static string strConLocal = @"Data Source=DESKTOP-LJ7F244\SQLEXPRESS;Initial Catalog=ShiptsDB;Integrated Security=True";
        static string strConLIVEDNS = @"Data Source=185.60.170.14;Integrated Security=False;User ID=site04;Password=aaF4j34&;";
        static WorkerDB()
        {
            if (local)
            {
                strCon = strConLocal;
            }
            else
            {
                strCon = strConLIVEDNS;
            }
        }
        public static List<Worker> GetAllWorkers()
        {
            List<Worker> Wl = new List<Worker>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM UserTB1", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Worker s = new Worker((int)reader["WorkerID"],
                    (string)reader["Name"], (string)reader["FamilyName"], (string)reader["WorkerType"],
                    (string)reader["Email"], (string)reader["PhoneNumber"], (string)reader["Address"],
                    (string)reader["BirthdayDate"], (string)reader["Password"], (string)reader["PermissionType"]);
                Wl.Add(s);
            }
            comm.Connection.Close();
            return Wl;
        }
        public static Worker GetWorkerByEmailAndPassword(string email, string password)
        {
            Worker s = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM UserTB1" +
                $" WHERE Email='{email}' AND Password='{password}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                s = new Worker((int)reader["WorkerID"],
                   (string)reader["Name"], (string)reader["FamilyName"], (string)reader["WorkerType"],
                   (string)reader["Email"], (string)reader["PhoneNumber"], (string)reader["Address"],
                   (string)reader["BirthdayDate"], (string)reader["Password"], (string)reader["PermissionType"]);

            }
            comm.Connection.Close();
            return s;
        }
        public static Worker GetWorkerByEmail(string email)
        {
            Worker s = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM UserTB1" +
                $" WHERE Email='{email}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                s = new Worker((int)reader["WorkerID"],
                   (string)reader["Name"], (string)reader["FamilyName"], (string)reader["WorkerType"],
                   (string)reader["Email"], (string)reader["PhoneNumber"], (string)reader["Address"],
                   (string)reader["BirthdayDate"], (string)reader["Password"], (string)reader["PermissionType"]);
            }
            comm.Connection.Close();
            return s;
        }

        internal static Worker InsertWorkerToDb(Worker val)
        {
            if (GetWorkerByEmail(val.Email) != null) return null;
            Worker s = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" INSERT INTO UserTB1(Name, FamilyName, WorkerType, Email, PhoneNumber, " +
                $" Address, BirthdayDate, Password, PermissionType )" +
                $" VALUES('{val.Name}', '{val.FamilyName}', '{val.WorkerType}', '{val.Email}'," +
                $"'{val.PhoneNumber}','{val.Address}','{val.BirthdayDate}','{val.Password}','{val.PermissionType}')", con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            if (res == 1)
            {
                s = GetWorkerByEmail(val.Email);
            }
            return s;
        }

        private static int ExcNonQ(string comm2Run)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            return res;
        }

        public static List<Worker> ExcReader(string comm2Run)
        {
            List<Worker> sl = new List<Worker>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Worker s = new Worker((int)reader["WorkerID"],
                    (string)reader["Name"], (string)reader["FamilyName"], (string)reader["WorkerType"],
                    (string)reader["Email"], (string)reader["PhoneNumber"], (string)reader["Address"],
                    (string)reader["BirthdayDate"], (string)reader["Password"], (string)reader["PermissionType"]);
                sl.Add(s);
            }
            comm.Connection.Close();
            return sl;
        }
        public static int UpdateWorker(Worker s)
        {
            string strComm =
                  $" UPDATE UserTB1 SET " +
                  $" Password={s.Password} " +
                  $" WHERE Email='{s.Email}'";

            return ExcNonQ(strComm);
        }
    }
}
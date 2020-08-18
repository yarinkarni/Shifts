using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace Shifts.Models
{
    public class ShiftsDB
    {
        static bool local = false;
        static string strCon = null;
        static string strConLocal = @"Data Source=DESKTOP-LJ7F244\SQLEXPRESS;Initial Catalog=ShiptsDB;Integrated Security=True";
        static string strConLIVEDNS = @"Data Source=185.60.170.14;Integrated Security=False;User ID=site04;Password=aaF4j34&;";
        static ShiftsDB()
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

        public static List<Shift> GetAllShifts()
        {
            List<Shift> Wl = new List<Shift>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM shiftsWorker", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Shift s = new Shift((int)reader["ShiftID"],
                     (int)reader["Day"],(string)reader["Remarks"],(string)reader["Shift"],(string)reader["Name"]);
                Wl.Add(s);
            }
            comm.Connection.Close();
            return Wl;
        }
        internal static Shift InsertShiftToDb(Shift val)
        {
            if (GetShiftByDayAndName(val.Day,val.Name) != null) return null;
            Shift s = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" INSERT INTO shiftsWorker(Day, Remarks, Shift, Name )" +
                $" VALUES('{val.Day}', '{val.Remarks}', '{val.Shift1}', '{val.Name}')", con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            if (res == 1)
            {
                s = GetShiftByDayAndName(val.Day,val.Name);
            }
            return s;
        }
        public static Shift GetShiftByDayAndName(int day, string name)
        {
            Shift s = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM shiftsWorker" +
                $" WHERE Day={day} AND Name='{name}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                s = new Shift((int)reader["ShiftID"],
                    (int)reader["Day"], (string)reader["Remarks"], (string)reader["Shift"],
                    (string)reader["Name"]);
            }
            comm.Connection.Close();
            return s;
        }

        public static Shift GetShiftByDay(int day)
        {
            Shift s = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM shiftsWorker" +
                $" WHERE Day='{day}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                s = new Shift((int)reader["ShiftID"],
                    (int)reader["Day"], (string)reader["Remarks"], (string)reader["Shift"],
                    (string)reader["Name"]);
            }
            comm.Connection.Close();
            return s;
        }
        public static int UpdateShifts(Shift s)
        {
             string strComm =
                  $" UPDATE shiftsWorker SET " +
                  $" Name='{s.Name}' " +
                  $" WHERE ShiftID={s.ShiftID}";

            return ExcNonQ(strComm);
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

        public static List<Shift> ExcReader(string comm2Run)
        {
            List<Shift> sl = new List<Shift>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Shift s =  new Shift((int)reader["ShiftID"],
                    (int)reader["Day"], (string)reader["Remarks"],  (string)reader["Shift"],
                    (string)reader["Name"]);
                sl.Add(s);
            }
            comm.Connection.Close();
            return sl;
        }
    }
}

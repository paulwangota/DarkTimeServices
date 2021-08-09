using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOperations;
using DarkTimeServices.Models;

namespace DarkTimeServices.DataAccess
{
    public static class Operations
    {
        public static bool IsValid(string reporterName, string department,
             DateTime receivedDTS)
        {
            return (!string.IsNullOrEmpty(reporterName) && !string.IsNullOrEmpty(department)
                && receivedDTS.Year > 1900);
        }

        public static bool IsValid(DarkTimeEntered data)
        {
            if (data == null) return false;
            return IsValid(data.ReporterName, data.Room,
                data.ReceivedDTS);
        }

        public static List<DarkTimeEntered> GetDarkTimeEnteredList(DateTime startDTS, DateTime endDTS)
        {
            List<DarkTimeEntered> list = new List<DarkTimeEntered>();
            ADODarkTime ado = null;
            try
            {
                ado = new ADODarkTime();
                list = ado.GetDarkTimeEnteredList(startDTS, endDTS);
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                list = new List<DarkTimeEntered>();
            }
            finally
            {
                ado = null;
            }
            return list;
        }

        public static void CreateDarkTimeEntered(string reporterName,
                 string room, DateTime receivedDTS, int size, int periodMinutes)
        {
            if (IsDarkTimeEnteredExist(reporterName, room, receivedDTS))
            {
                /*Audit.Log("Record: " + reporterName + "; "
                    + room + "; "
                    + ADODarkTime.DateTimeDisplay(receivedDTS)
                    + " already exists.");*/
                return;
            }
            ADODarkTime ado = null;
            try
            {
                ado = new ADODarkTime();
                ado.InsertDarkTimeEntered(reporterName, room,
                    receivedDTS, size, periodMinutes);
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
            }
            finally
            {
                ado = null;
            }
        }
        public static bool IsDarkTimeEnteredExist(string reporterName,
           string room, DateTime receivedDTS)
        {
            ADODarkTime ado = null;
            DarkTimeEntered data = null;
            try
            {
                ado = new ADODarkTime();
                data
                    = ado.GetDarkTimeEntered(reporterName, room, receivedDTS);
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                data = null;
            }
            finally
            {
                ado = null;
            }
            return (data != null);
        }
    }
}

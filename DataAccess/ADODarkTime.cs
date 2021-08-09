using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LogOperations;
using DarkTimeServices.Models;

namespace DarkTimeServices.DataAccess
{
    public class ADODarkTime: ADOObject
    {
        protected override void DefaultConnection()
        {
            SetConnection(Config.PARAM("DataHubDBServer"), Config.PARAM("DataHubDBName"));
        }

        private DarkTimeEntered SetDarkTimeEntered(SqlDataReader r)
        {
            if (r == null) return null;
            DarkTimeEntered data = new DarkTimeEntered();
            try
            {
                data.SizeKB = 0;
                data.ReporterName = r["Reporter"].ToString();
                data.Room = r["Room"].ToString();
                data.ReceivedDTS = DateTime.Parse(r["ReceivedDTS"].ToString());
                data.PeriodMinutes = int.Parse(r["PeriodMinutes"].ToString());
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                data = null;
            }
            return data;
        }

        /*public List<DarkTimeEntered> GetDarkTimeEnteredList()
        {
            List<DarkTimeEntered> list = new List<DarkTimeEntered>();
            try
            {
                DefaultConnection();
                SQLText
                    = SQLStatements.GetDarkTimeEnteredListSQL();
                Open();
                while (ReaderIterator.Read())
                    list.Add(SetDarkTimeEntered(ReaderIterator));
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                list = new List<DarkTimeEntered>();
            }
            finally
            {
                Close();
            }
            return list;
        }*/

        public List<DarkTimeEntered> GetDarkTimeEnteredList(DateTime startDTS, DateTime endDTS)
        {
            List<DarkTimeEntered> list = new List<DarkTimeEntered>();
            try
            {
                DefaultConnection();
                SQLText
                    = SQLStatements.GetDarkTimeEnteredListSQL(startDTS, endDTS);
                Open();
                while (ReaderIterator.Read())
                    list.Add(SetDarkTimeEntered(ReaderIterator));
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                list = new List<DarkTimeEntered>();
            }
            finally
            {
                Close();
            }
            return list;
        }

        public DarkTimeEntered GetDarkTimeEntered(DarkTimeEntered data)
        {
            DarkTimeEntered result = null;
            if (data == null || !Operations.IsValid(data))
            {
                Audit.LogError("GetDarkTimeEntered() failed validation.");
                return null;
            }
            /*Audit.Log("Reporter: " + data.ReporterName
                + "; Room: " + data.Room
                + "; ReceivedDTS: " + ADOObject.DateTimeDisplay(data.ReceivedDTS)
                + "; PeriodMinutes: " + data.PeriodMinutes.ToString());*/
            try
            {
                DefaultConnection();
                SQLText
                    = SQLStatements.GetDarkTimeEnteredSQL(data);
                OpenSingleQuery();
                if (ReaderIterator.Read())
                    result = SetDarkTimeEntered(ReaderIterator);
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                result = null;
            }
            finally
            {
                Close();
            }
            /*if (result == null)
                Audit.Log("No DarkTimeEntered data found.");
            else
                Audit.Log("DarkTimeEntered data found.");*/
            return result;
        }

        public DarkTimeEntered GetDarkTimeEntered(string reporterName,
            string room, DateTime receivedDTS)
        {
            DarkTimeEntered data = new DarkTimeEntered();
            data.ReporterName = reporterName;
            data.Room = room;
            data.ReceivedDTS = receivedDTS;
            if (!Operations.IsValid(data))
            {
                Audit.LogError("GetDarkTimeEntered() failed validation.");
                return null;
            }
            return GetDarkTimeEntered(data);
        }

        /*public void DeleteDarkTimeEntered()
        {
            try
            {
                DefaultConnection();
                SQLText = SQLStatements.DeleteDarkTimeEnteredSQL();
                Update();
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
            }
            finally
            {
                Close();
            }
        }*/

        public void InsertDarkTimeEntered(DarkTimeEntered data)
        {
            if (data == null || !Operations.IsValid(data))
            {
                Audit.LogError("InsertDarkTimeEntered() failed validation.");
                return;
            }
            bool success = false;
            try
            {
                DefaultConnection();
                SQLText = SQLStatements.InsertDarkTimeEnteredSQL(data);
                Update();
                success = true;
            }
            catch (Exception e)
            {
                Audit.LogError(e.Message);
                success = false;
            }
            finally
            {
                Close();
            }
            if (success)
                Audit.Log("DarkTimeEntered creation successful.");
            else
                Audit.LogError("DarkTimeEntered creation FAILED.");
        }

        public void InsertDarkTimeEntered(string reporterName,
            string room, DateTime receivedDTS, int size, int periodMinutes)
        {
            DarkTimeEntered data = new DarkTimeEntered();
            data.ReporterName = reporterName;
            data.ReceivedDTS = receivedDTS;
            data.Room = room;
            data.SizeKB = size;
            data.PeriodMinutes = periodMinutes;
            if (!Operations.IsValid(data))
            {
                Audit.LogError("InsertDarkTimeEntered() failed validation.");
                return;
            }
            InsertDarkTimeEntered(data);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOperations;
using DarkTimeServices.Models;
namespace DarkTimeServices.DataAccess
{
   public static class SQLStatements
    {
        public static string GetRawDarkTimeDataListSQL()
        {
            return "SELECT [Reporter], ISNULL([Subject], '') AS 'Subject', "
                + "ReceivedDTS, ISNULL([SizeTimeRaw], '') AS 'SizeTimeRaw' "
                + "FROM RawDarkTime "
                + "GROUP BY  [Reporter], [Subject], ReceivedDTS, [SizeTimeRaw] "
                + "ORDER BY ReceivedDTS";
        }

        public static string DeleteDarkTimeEnteredSQL()
        {
            return "DELETE FROM DarkTimeEntered";
        }

        public static string InsertDarkTimeEnteredSQL(DarkTimeEntered data)
        {
            if (data == null || !Operations.IsValid(data))
                return null;
            string sql = "INSERT INTO DarkTimeEntered (Reporter, Room, ReceivedDTS, SizeKB, PeriodMinutes) ";
            sql += "VALUES('" + data.ReporterName + "', ";
            sql += "'" + data.Room + "', ";
            sql += ADOObject.DateTimeToDBStr(data.ReceivedDTS) + ", ";
            sql += ADOObject.ZeroToNullDB(data.SizeKB) + ", ";
            sql += ADOObject.ZeroToNullDB(data.PeriodMinutes) + ")";
            return sql;
        }

        public static string GetDarkTimeEnteredListSQL()
        {
            return "SELECT Reporter, ISNULL(Room, '') AS 'Room', "
                + "ReceivedDTS, ISNULL(SizeKB, 0) AS 'SizeKB', "
                + "ISNULL(PeriodMinutes, 0) AS 'PeriodMinutes' "
                + "FROM DarkTimeEntered "
                + "ORDER BY Room, ReceivedDTS, Reporter";
        }

        public static string GetDarkTimeEnteredListSQL(DateTime startDTS, DateTime endDTS)
        {
            string sql = "SELECT Reporter, ISNULL(Room, '') AS 'Room', "
                + "ReceivedDTS, ISNULL(SizeKB, 0) AS 'SizeKB', "
                + "ISNULL(PeriodMinutes, 0) AS 'PeriodMinutes' "
                + "FROM DarkTimeEntered ";
            if (startDTS.Year > 1900 || endDTS.Year > 1900)
            {
                sql += "WHERE ";
                if (startDTS.Year > 1900)
                {
                    sql += "ReceivedDTS >= " + ADOObject.DateTimeToDBStr(startDTS) + " ";
                    if (endDTS.Year > 1900)
                        sql += "AND ReceivedDTS <= " + ADOObject.DateTimeToDBStr(endDTS) + " ";
                }
                else
                {
                    if (endDTS.Year > 1900)
                        sql += "ReceivedDTS <= " + ADOObject.DateTimeToDBStr(endDTS) + " ";
                }
            }
            sql += "ORDER BY Room, ReceivedDTS, Reporter";
            return sql;
        }

        public static string GetDarkTimeEnteredSQL(DarkTimeEntered data)
        {
            if (data == null || !Operations.IsValid(data))
                return null;
            string sql = "SELECT Reporter, Room, ReceivedDTS, "
                + "ISNULL(SizeKB, 0) AS 'SizeKB', "
                + "PeriodMinutes FROM DarkTimeEntered "
                + "WHERE Reporter = '" + data.ReporterName + "' "
                + "AND Room = '" + data.Room + "' "
                + "AND ReceivedDTS = " + ADOObject.DateTimeToDBStr(data.ReceivedDTS) + " ";
            //+ "AND PeriodMinutes = " + data.PeriodMinutes.ToString();
            return sql;
        }
    }
}

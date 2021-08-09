using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Microsoft.Exchange.WebServices.Data;
using System.IO;
using System.Text.RegularExpressions;
using DarkTimeServices.DataAccess;

namespace DarkTimeServices.Mail
{
    public class MsExchangeMail
    {
        private static string EMAIL_DARKTIME_OPERATORS = "DTOperator@sonomacourt.org";
        private static string USER_NAME_DARKTIME_OPERATORS = "DTOperator";
        private static string PASSWORD_DARKTIME_OPERATORS = "items-D45VghQ";
        private static string COURT_DOMAIN = "sonomacourt.org";
        public static void ReadDarkTimeEmails()
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, -1, 0, 0);
                DateTime date = DateTime.Now.Add(ts);
                EmailMessage message = null;
                ExchangeService exchange = new ExchangeService();
                exchange.Credentials = new WebCredentials(USER_NAME_DARKTIME_OPERATORS, PASSWORD_DARKTIME_OPERATORS, COURT_DOMAIN);
                exchange.TraceEnabled = true;
                exchange.TraceFlags = TraceFlags.All;
                exchange.AutodiscoverUrl(EMAIL_DARKTIME_OPERATORS, RedirectionCallback);
                SearchFilter.IsGreaterThanOrEqualTo filter = new SearchFilter.IsGreaterThanOrEqualTo(ItemSchema.DateTimeReceived, date);
                if (exchange != null)
                {
                    FindItemsResults<Item> findResults = exchange.FindItems(WellKnownFolderName.Inbox, filter, new ItemView(100));// top 100 emails
                    foreach (Item item in findResults.Items)
                    {
                        message = EmailMessage.Bind(exchange, item.Id);
                        string dateRecieve = message.DateTimeReceived.ToString();
                        string fromUser = message.From.Name.ToString();
                        string fromEmail = message.From.Address.ToString();
                        string subject = message.Subject;
                        string courtDepartment = CourtDepartment(subject);
                        string messageId = message.Id.ToString();
                        string messagebody = message.Body.Text;
                        int darkTimeMinutes = ComputeDarkTimeMinutes(message.DateTimeReceived);
                        if (!string.IsNullOrEmpty(subject))
                        {
                            if (!string.IsNullOrEmpty(courtDepartment))
                            {
                                Operations.CreateDarkTimeEntered(fromUser, courtDepartment, Convert.ToDateTime(dateRecieve), 0, darkTimeMinutes);
                            }
                        }

                        //DateTime dt = new DateTime(recievedDate.Year, recievedDate.Month, recievedDate.Day, recievedDate.Hour, recievedDate.Minute, 0);
                        //DateTime morningDarkTime = new DateTime(dt.Year, dt.Month, dt.Day, 8, 30, 0);
                        //DateTime eveningDarkTime = new DateTime(dt.Year, dt.Month, dt.Day, 13, 30, 0);
                        //int minutes = (int)dt.Subtract(morningDarkTime).TotalMinutes;                  

                        message = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Audit.LogError("Reading PTROrders inbox encounted an error: " + ex.Message);
            }
            finally { }
        }


        private static bool RedirectionCallback(string url)
        {
            bool isValid = false;
            Uri redirectionUri = new Uri(url);
            if (redirectionUri.Scheme == "https")
                isValid = true;
            return isValid;
        }


        private static string CourtDepartment(string department)
        {
            string value = string.Empty;
            string[] Departments = {
               " 1 ", " 2 ", " 3 ", " 4 ", " 5 ", " 6 ", " 7 ", " 8 ", " 9 ", " 10 ", " 11 ", " 12 ", " 13 ", " 14 ", " 15 ", " 16 ", " 17 ", " 18 ", " 19 ", " 20 ", " 21 ", " 22 ", " 23 ", " 24 ", " 25 ", " 26 ", " 27 ", " 28 ", " 29 ", " 30 ",
               // "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13", "D14", "D15", "D16", "D17", "D18", "D19", "D20", "D21", "D22", "D23", "D24", "D25", "D26", "D27", "D28", "D29", "D30",
               //  "Dept 1", "Dept 2", "Dept 3", "Dept 4", "Dept 5", "Dept 6", "Dept 7", "Dept 8", "Dept 9", "Dept 10", "Dept 11", "Dept 12", "Dept 13", "Dept 14", "Dept 15", "Dept 16", "Dept 17", "Dept 18", "Dept 19", "Dept 20", "Dept 21", "Dept 22", "Dept 23", "Dept 24", "Dept 25", "Dept 26", "Dept 27", "Dept 28", "Dept 29", "Dept 30",
               //  "Dept. 1", "Dept. 2", "Dept. 3", "Dept. 4", "Dept. 5", "Dept. 6", "Dept. 7", "Dept. 8", "Dept. 9", "Dept. 10", "Dept. 11", "Dept. 12", "Dept. 13", "Dept. 14", "Dept. 15", "Dept. 16", "Dept. 17", "Dept. 18", "Dept. 19", "Dept. 20", "Dept. 21", "Dept. 22", "Dept. 23", "Dept. 24", "Dept. 25", "Dept. 26", "Dept. 27", "Dept. 28", "Dept. 29", "Dept. 30"
            };
            // string room = Regex.Match(department, "^[1-9]\\d{0,30}$", RegexOptions.IgnorePatternWhitespace).Value;
            string courtroom = department.ToLower().Replace("d", " ").Replace("/", " ").Replace("'", " ");
            string dept = Departments.FirstOrDefault<string>(court => courtroom.ToUpper().Contains(court));

            switch (dept.Trim().ToUpper())
            {
                #region Court Departments               
                case "1":
                    {
                        value = "D01";
                    }
                    break;
                case "2":
                    {
                        value = "D02";
                    }
                    break;
                case "3":
                    {
                        value = "D03";
                    }
                    break;
                case "4":
                    {
                        value = "D04";
                    }
                    break;
                case "5":
                    {
                        value = "D05";
                    }
                    break;
                case "6":
                    {
                        value = "D06";
                    }
                    break;
                case "7":
                    {
                        value = "D07";
                    }
                    break;
                case "8":
                    {
                        value = "D08";
                    }
                    break;
                case "9":
                    {
                        value = "D09";
                    }
                    break;
                case "10":
                    {
                        value = "D10";
                    }
                    break;
                case "11":
                    {
                        value = "D11";
                    }
                    break;
                case "12":
                    {
                        value = "D12";
                    }
                    break;
                case "13":
                    {
                        value = "D13";
                    }
                    break;
                case "14":
                    {
                        value = "D14";
                    }
                    break;
                case "15":
                    {
                        value = "D15";
                    }
                    break;
                case "16":
                    {
                        value = "D16";
                    }
                    break;
                case "17":
                    {
                        value = "D17";
                    }
                    break;
                case "18":
                    {
                        value = "D18";
                    }
                    break;
                case "19":
                    {
                        value = "D19";
                    }
                    break;
                case "20":
                    {
                        value = "D20";
                    }
                    break;
                case "21":
                    {
                        value = "D21";
                    }
                    break;
                case "22":
                    {
                        value = "D22";
                    }
                    break;
                case "23":
                    {
                        value = "D23";
                    }
                    break;
                case "D24":
                case "24":
                    {
                        value = "D24";
                    }
                    break;
                case "25":
                    {
                        value = "D25";
                    }
                    break;
                case "26":
                    {
                        value = "D26";
                    }
                    break;
                case "27":
                    {
                        value = "D27";
                    }
                    break;
                case "28":
                    {
                        value = "D28";
                    }
                    break;
                case "29":
                    {
                        value = "D29";
                    }
                    break;
                case "30":
                    {
                        value = "D30";
                    }
                    break;
                    #endregion
            }
            return value;
        }

        private static int ComputeDarkTimeMinutes(DateTime dt)
        {
            int minutes = 0;

            DateTime mDarkTime = new DateTime(dt.Year, dt.Month, dt.Day, 13, 30, 0);
            DateTime eDarkTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0);
            try
            {
                int result = DateTime.Compare(dt, mDarkTime);
                if (result == 0 || result < 0)
                {
                    // moring
                    DateTime morningDarkTime = new DateTime(dt.Year, dt.Month, dt.Day, 8, 30, 0);// morning dark time =8:30 AM constant
                    minutes = (int)dt.Subtract(morningDarkTime).TotalMinutes;
                }
                else
                {
                    // evening
                    DateTime eveningDarkTime = new DateTime(dt.Year, dt.Month, dt.Day, 13, 30, 0); // evening dark time =1:30 PM constant
                    minutes = (int)dt.Subtract(eveningDarkTime).TotalMinutes;
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return minutes;
        }

    }
}

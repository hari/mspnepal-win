using System;
using Windows.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using SQLite.Net;
using System.Linq;
using Windows.Storage; 

namespace MSP_Nepal
{
    class MspList
    {
        const string APP_URL = "https://api.myjson.com/bins/3r0jd", FIRST_RUN = "first_run";

        private static SQLiteConnection conn;
        /// <summary>
        /// checks if the application's first run value is set
        /// </summary>
        /// <returns>true if first_run is not set</returns>
        public static bool IsFirstRun()
        {
            return ApplicationData.Current.LocalSettings.Values[FIRST_RUN] == null;
        }
        /// <summary>
        /// sets the value of first_run in localsetting
        /// </summary>
        /// <param name="value">new value to set</param>
        public static void SetFirstRun(bool value)
        {
            ApplicationData.Current.LocalSettings.Values[FIRST_RUN] = value;
        }


        /// <summary>
        /// gets connection object that can be used to communicate with db
        /// </summary>
        /// <returns>SqLiteConnection object</returns>
        private static SQLiteConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(),
                    System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "MSPList.sqlite"));
            }
            return conn;
        }
        /// <summary>
        /// Creates the table if not exists
        /// </summary>
        /// <returns>1 if table is created</returns>
        public static int CreateTable()
        {
            return GetConnection().CreateTable<MSP>();
        }
        /// <summary>
        /// bulk adds list of MSP
        /// </summary>
        /// <param name="msps">list of MSPs to add</param>
        /// <returns>count of added rows</returns>
        public static int AddAll(List<MSP> msps)
        {
            return GetConnection().InsertAll(msps);
        }

        /// <summary>
        /// adds new MSP to the database
        /// </summary>
        /// <param name="msp">an instance of MSP to insert in db</param>
        /// <returns>index of the inserted row</returns>
        public static int AddMSP(MSP msp)
        {
            return GetConnection().Insert(msp);
        }

        /// <summary>
        /// Returns the rows of MSPs in database
        /// </summary>
        /// <returns>List of MSP</returns>
        public static List<MSP> GetMSP()
        {
            return GetConnection().Table<MSP>().ToList();
        }

        /// <summary>
        /// Downloads that from server
        /// </summary>
        /// <returns>Response from server</returns>
        public static async Task<string> GetData()
        {
            return await new HttpClient().GetStringAsync(new Uri(APP_URL));
        }

    }
}

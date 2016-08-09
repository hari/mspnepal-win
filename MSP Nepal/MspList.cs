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

        public static bool IsFirstRun()
        {
            return ApplicationData.Current.LocalSettings.Values[FIRST_RUN] == null;
        }

        public static void SetFirstRun(bool value)
        {
            ApplicationData.Current.LocalSettings.Values[FIRST_RUN] = value;
        }

        private static SQLiteConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(),
                    System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MspList.sqlite"));
            }
            return conn;
        }

        public static int CreateTable()
        {
            return GetConnection().CreateTable<MSP>();
        }

        public static int AddAll(List<MSP> msps)
        {
            return GetConnection().InsertAll(msps);
        }

        public static int AddMSP(MSP msp)
        {
            return GetConnection().Insert(msp);
        }

        public static List<MSP> GetMSP()
        {
            return (from msp in GetConnection().Table<MSP>() select msp).ToList();

        }

        public static async Task<string> GetData()
        {
            return await new HttpClient().GetStringAsync(new Uri(APP_URL));
        }

    }
}

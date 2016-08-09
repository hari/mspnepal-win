using System.Net.Http;
using System.Threading.Tasks;

namespace MSP_Nepal
{
    class MspList
    {
        const string APP_URL = "http://myjson.com/3r0jd", FIRST_RUN = "first_run";

        public bool IsFirstRun()
        {
            var data = Windows.Storage.ApplicationData.Current.LocalSettings;
            return data.Values[FIRST_RUN] == null;
        }

        public void SetFirstRun(bool value)
        {
            var data = Windows.Storage.ApplicationData.Current.LocalSettings;
            data.Values[FIRST_RUN] = value;
        }
        public static void CreateDataBase()
        {
            var path = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "LisDb.sqlite");
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))

            {

                conn.CreateTable<MSP>();

            }
        }

        public Task<HttpResponseMessage> GetData()
        {
            HttpClient client = new HttpClient();
            return client.GetAsync(APP_URL);
        }

    }
}

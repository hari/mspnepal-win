using Windows.UI.Xaml.Controls;
using Windows.Data.Json;
using System.Collections.Generic;
using System;

namespace MSP_Nepal
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if (MspList.IsFirstRun())
            {
                ShowLoading();
                LoadAndSave();
                MspList.SetFirstRun(false);
            }

        }
        private void ShowLoading()
        {

        }

        /// <summary>
        /// Downloads that from server and then stores it in our local database
        /// </summary>
        public async void LoadAndSave()
        {
            List<MSP> list = new List<MSP>();
            JsonArray jsonList = JsonArray.Parse(await MspList.GetData());
            for(int i = jsonList.Count - 1;i >= 0; --i)
            {
                
                JsonObject obj = jsonList.GetObjectAt((uint)i);
                list.Add(new MSP(
                     obj.GetNamedString("full_name"),
                     obj.GetNamedString("college"),
                     string.Empty
                    ));
            }
            MspList.AddAll(list);
        }
        
    }
}

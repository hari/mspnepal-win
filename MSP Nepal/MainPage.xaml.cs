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
                LoadAndSave();
                MspList.SetFirstRun(false);
            }
            for (char a = 'A'; a <= 'Z'; a++)
            {
                var b = new TextBlock();
                b.Text = a.ToString();
                b.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                b.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 100, 100, 200));
                b.PointerPressed += (s, e) =>
                {
                    Show((s as TextBlock).Text, true);
                };
                A2Z.Children.Add(b);
            }
            Show();
        }

        private void Show(string Str = null, bool Filter = false)
        {
            ListMsp.Items.Clear();
            if (Str != null)
            {
                Str = Str.ToLower();
            }
            MspList.GetMSP().ForEach(msp =>
            {
                MspListItem mlt = null;
                if (Str == null)
                {
                    mlt = new MspListItem(msp.FullName, msp.College);
                }
                else
                {
                    if (Filter && (msp.FullName.ToLower().Substring(0,1).Equals(Str) || msp.College.ToLower().Substring(0, 1).Equals(Str)))
                    {
                        mlt = new MspListItem(msp.FullName, msp.College);
                    }
                    if (!Filter && (msp.FullName.ToLower().Contains(Str) || msp.College.ToLower().Contains(Str)))
                    {
                        mlt = new MspListItem(msp.FullName, msp.College);
                    }
                }
                if (mlt != null) ListMsp.Items.Add(mlt);
            });
            if (ListMsp.Items.Count == 0)
            {
                ListMsp.Items.Add(new MspListItem("Ooops!", "No result found."));
            }
        }

        /// <summary>
        /// Downloads that from server and then stores it in our local database
        /// </summary>
        public async void LoadAndSave()
        {
            List<MSP> list = new List<MSP>();
            JsonArray jsonList = JsonArray.Parse(await MspList.GetData());
            for (int i = jsonList.Count - 1; i >= 0; --i)
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

        private async void BtnSearch_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (TxtSearch.Text.Trim().Length == 0)
            {
                Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog("Nothing to search");
                await msg.ShowAsync();
            }
            Show(TxtSearch.Text);
        }

        private void TxtSearch_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (TxtSearch.Text.Length == 0) Show();
        }

        private void TxtSearch_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Enter") && TxtSearch.Text.Trim().Length > 0)
            {
                Show(TxtSearch.Text);
            }
        }
    }
}

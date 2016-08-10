using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using System;

namespace MSP_Nepal
{
    /// <summary>
    /// This class will represent the listviewitem style of the MSP item in the listview
    /// </summary>
    partial class MspListItem : Page
    {
        public MspListItem(string name, string college)
        {
            InitializeComponent();
            TxtName.Text = name;
            TxtName.Foreground = new SolidColorBrush(Color.FromArgb(240, 40, 40, 40));
            TxtCollege.Text = college;
            TxtCollege.Foreground = new SolidColorBrush(Color.FromArgb(190, 40, 40, 40));
            BrdFirstName.Background = GetBackground(name, college);
            TxtFirstName.Text = name.Substring(0, 1);
        }

        private Brush GetBackground(string name, string college)
        {
            string[] colors = new string[]{"#1abc9c", "#2ecc71", "#3498db", "#9b59b6", "#34495e", "#16a085",
                "#27ae60", "#2980b9", "#8e44ad", "#2c3e50", "#f1c40f", "#e67e22", "#e74c3c", "#ecf0f1",
                "#95a5a6", "#f39c12", "#d35400", "#c0392b", "#bdc3c7", "#7f8c8d"};
            string current = colors[(name.Length + college.Length) % colors.Length];
            return new SolidColorBrush(
                Color.FromArgb(255,
                byte.Parse(current.Substring(1, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(current.Substring(3, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(current.Substring(5, 2), System.Globalization.NumberStyles.HexNumber)
                ));
        }
    }
}
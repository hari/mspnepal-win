using Windows.UI.Xaml.Controls;

namespace MSP_Nepal
{
    /// <summary>
    /// This class will represent the listviewitem style of the MSP item in the listview
    /// </summary>
    internal class MspListItem : StackPanel
    {
        private TextBlock TxtName = new TextBlock(), TxtCollege = new TextBlock();
        public MspListItem(string name, string college)
        {
            Padding = new Windows.UI.Xaml.Thickness(0, 4, 0, 4);
            TxtName.Text = name;
            TxtName.FontSize = 20;
            TxtName.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(240, 40, 40, 40));
            TxtCollege.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(190, 40, 40, 40));
            TxtCollege.Text = college;
            TxtCollege.FontSize = 13;
            Children.Add(TxtName);
            Children.Add(TxtCollege);
        }
    }
}
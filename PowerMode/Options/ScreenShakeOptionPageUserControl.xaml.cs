namespace BigEgg.Tools.PowerMode.Options
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ScreenShakeOptionPageUserControl.xaml
    /// </summary>
    public partial class ScreenShakeOptionPageUserControl : UserControl
    {
        public ScreenShakeOptionPageUserControl(ScreenShakeOptionPage page)
        {
            InitializeComponent();

            DataContext = page.Settings;
        }
    }
}

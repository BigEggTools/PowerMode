namespace BigEgg.Tools.PowerMode.Options
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ComboModeOptionPageUserControl.xaml
    /// </summary>
    public partial class ComboModeOptionPageUserControl : UserControl
    {
        public ComboModeOptionPageUserControl(ComboModeOptionPage page)
        {
            InitializeComponent();

            DataContext = page.Settings;
        }
    }
}

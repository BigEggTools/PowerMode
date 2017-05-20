namespace BigEgg.Tools.PowerMode.Options
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for GeneralOptionPageUserControl.xaml
    /// </summary>
    public partial class GeneralOptionPageUserControl : UserControl
    {
        public GeneralOptionPageUserControl(GeneralOptionPage page)
        {
            InitializeComponent();

            DataContext = page.Settings;
        }
    }
}

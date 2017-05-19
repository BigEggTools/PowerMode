using System.Windows.Controls;

namespace BigEgg.Tools.PowerMode.Options
{
    /// <summary>
    /// Interaction logic for GeneralOptionPageUserControl.xaml
    /// </summary>
    public partial class GeneralOptionPageUserControl : UserControl
    {
        public GeneralOptionPageUserControl(GeneralOptionPageSettings generalOptionPageSettings)
        {
            InitializeComponent();

            DataContext = generalOptionPageSettings;
        }
    }
}

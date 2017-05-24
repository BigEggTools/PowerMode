namespace BigEgg.Tools.PowerMode.Options
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ParticlesOptionPageUserControl.xaml
    /// </summary>
    public partial class ParticlesOptionPageUserControl : UserControl
    {
        public ParticlesOptionPageUserControl(ParticlesOptionPage page)
        {
            InitializeComponent();

            DataContext = page.Settings;
        }
    }
}

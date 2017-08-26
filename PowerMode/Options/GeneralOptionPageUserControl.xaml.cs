namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.Windows.Controls;

    using BigEgg.Tools.PowerMode.Options.ViewModels;

    /// <summary>
    /// Interaction logic for GeneralOptionPageUserControl.xaml
    /// </summary>
    public partial class GeneralOptionPageUserControl : UserControl
    {
        private Action resetMaxComboStreak;


        public GeneralOptionPageUserControl(GeneralOptionPage page)
        {
            InitializeComponent();

            DataContext = new GeneralOptionViewModel()
            {
                Settings = page.Settings,
                Achievements = page.Achievements
            };

            resetMaxComboStreak = () =>
            {
                page.ResetMaxComboStreak();
            };
        }


        private void ResetMaxComboStreak(object sender, System.Windows.RoutedEventArgs e)
        {
            resetMaxComboStreak.Invoke();
        }
    }
}

namespace BigEgg.Tools.PowerMode.Adornments
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.Text.Editor;
    using System.Windows.Controls;

    public partial class ComboAdornment
    {
        private const int ADORNMENT_WIDTH = 100;
        private const int ADORNMENT_TITLE_HEIGHT = 15;
        private const int ADORNMENT_COMBO_NUMBER_HEIGHT = 65;
        private const int ADORNMENT_EXCLAMATION_HEIGHT = 30;
        private const double TopMargin = 30;
        private const double RightMargin = 30;
        private Image titleImage;
        private Image comboNumberImage;
        private Image exclamationImage;


        public ComboAdornment()
        {
            this.titleImage = new Image()
            {
                Source = UpdateTitleImage()
            };
            this.comboNumberImage = new Image()
            {
                Source = UpdateComboNumberImage(400)
            };
            this.exclamationImage = new Image()
            {
                Source = UpdateExclamationImage(300)
            };
        }

        public List<Image> OnSizeChanged(IWpfTextView view)
        {
            Canvas.SetLeft(this.titleImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(this.titleImage, view.ViewportTop + TopMargin);

            Canvas.SetLeft(this.comboNumberImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(this.comboNumberImage, view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT);

            Canvas.SetLeft(this.exclamationImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(this.exclamationImage, view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT + ADORNMENT_COMBO_NUMBER_HEIGHT);

            return new List<Image>()
            {
                titleImage,
                comboNumberImage,
                exclamationImage
            };
        }
    }
}

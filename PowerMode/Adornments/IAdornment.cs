namespace BigEgg.Tools.PowerMode.Adornments
{
    using Microsoft.VisualStudio.Text.Editor;

    public interface IAdornment
    {
        void OnSizeChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount);

        void OnTextBufferChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount);

        void Cleanup(IAdornmentLayer adornmentLayer, IWpfTextView view);
    }
}

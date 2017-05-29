namespace BigEgg.Tools.PowerMode.Options.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    using BigEgg.Tools.PowerMode.Utils;

    [TemplatePart(Name = PART_ColorPickerToggleButton, Type = typeof(ToggleButton))]
    [TemplatePart(Name = PART_ColorPickerPopup, Type = typeof(Popup))]
    public class ColorPicker : Control
    {
        private const string PART_ColorPickerToggleButton = "PART_ColorPickerToggleButton";
        private const string PART_ColorPickerPopup = "PART_ColorPickerPopup";

        private ToggleButton toggleButton;
        private Popup popup;
        private Color? originColor;


        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }


        public ColorPicker()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }


        #region Dependency Properties
        #region IsOpen
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(ColorPicker), new UIPropertyMetadata(false, OnIsOpenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
            {
                colorPicker.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        private void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue) { originColor = SelectedColor; }
            else { SelectedColor = originColor; }

            RoutedEventArgs args = new RoutedEventArgs(newValue ? OpenedEvent : ClosedEvent, this);
            RaiseEvent(args);
        }
        #endregion

        #region Selected Color
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color?), typeof(ColorPicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSelectedColorPropertyChanged)));

        public Color? SelectedColor
        {
            get { return (Color?)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
            {
                colorPicker.OnSelectedColorChanged((Color?)e.OldValue, (Color?)e.NewValue);
            }
        }

        private void OnSelectedColorChanged(Color? oldValue, Color? newValue)
        {
            SelectedColorText = newValue.Value.ToDrawingColor().ToHexString();

            var args = new RoutedPropertyChangedEventArgs<Color?>(oldValue, newValue)
            {
                RoutedEvent = SelectedColorChangedEvent
            };
            RaiseEvent(args);
        }
        #endregion

        #region SelectedColorText
        public static readonly DependencyProperty SelectedColorTextProperty = DependencyProperty.Register("SelectedColorText", typeof(string), typeof(ColorPicker), new UIPropertyMetadata(""));

        public string SelectedColorText
        {
            get { return (string)GetValue(SelectedColorTextProperty); }
            protected set { SetValue(SelectedColorTextProperty, value); }
        }
        #endregion
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            toggleButton = this.Template.FindName(PART_ColorPickerToggleButton, this) as ToggleButton;
            popup = GetTemplateChild(PART_ColorPickerPopup) as Popup;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            CloseColorPicker(true);
        }

        #region Control Events Handlers
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen && e.Key == Key.Down)
            {
                OpenColorPick();
                e.Handled = true;
            }
            if (IsOpen && e.Key == Key.Up)
            {
                CloseColorPicker(true);
                e.Handled = true;
            }
            if (IsOpen && e.Key == Key.Escape)
            {
                CloseColorPicker(true);
                e.Handled = true;
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseColorPicker(true);
        }
        #endregion

        #region Events
        #region SelectedColorChangedEvent
        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color?>), typeof(ColorPicker));

        public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }
        #endregion

        #region OpenedEvent
        public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent("OpenedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPicker));

        public event RoutedEventHandler Opened
        {
            add { AddHandler(OpenedEvent, value); }
            remove { RemoveHandler(OpenedEvent, value); }
        }
        #endregion

        #region ClosedEvent
        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent("ClosedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPicker));

        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }
        #endregion
        #endregion

        private void OpenColorPick()
        {
            IsOpen = true;
        }

        private void CloseColorPicker(bool isFocusOnColorPicker)
        {
            if (IsOpen) { IsOpen = false; }
            ReleaseMouseCapture();
            if (isFocusOnColorPicker && (toggleButton != null)) { toggleButton.Focus(); }
        }
    }
}

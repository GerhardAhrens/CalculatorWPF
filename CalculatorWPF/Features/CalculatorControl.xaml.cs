namespace System.Windows.Calculator
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaktionslogik für CalculatorControl.xaml
    /// </summary>
    public partial class CalculatorControl : UserControl
    {
        private readonly CalculatorViewModel _viewModel = new();

        private readonly ClassicCalculatorView _classicView;
        private readonly ExpressionCalculatorView _expressionView;

        public CalculatorControl()
        {
            this.InitializeComponent();
            _viewModel.PropertyChanged += this.OnViewModelPropertyChanged;
            this._classicView = new ClassicCalculatorView();
            this._expressionView = new ExpressionCalculatorView();

            this._classicView.DataContext = _viewModel;
            this._expressionView.DataContext = _viewModel;

            this.Loaded += CalculatorControl_Loaded;
        }

        private void OnViewModelPropertyChanged(object sender, ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CalculatorViewModel.Result):

                    this.Result = _viewModel.Result;
                    break;
            }
        }

        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register(nameof(Result), typeof(double?), typeof(CalculatorControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty MemoryValueProperty =
            DependencyProperty.Register(nameof(MemoryValue), typeof(double?), typeof(CalculatorControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register(nameof(Mode),  typeof(CalculatorMode), typeof(CalculatorControl), new PropertyMetadata(CalculatorMode.Classic, OnModeChanged));
        
        public CalculatorMode Mode
        {
            get => (CalculatorMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
        public double? MemoryValue
        {
            get => (double?)GetValue(MemoryValueProperty);
            set => SetValue(MemoryValueProperty, value);
        }

        public double? Result
        {
            get => (double?)GetValue(ResultProperty);
            set => SetValue(ResultProperty, value);
        }

        private void CalculatorControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateMode();
        }

        private void UpdateMode()
        {
            PART_Content.Content = Mode == CalculatorMode.Classic ? _classicView : _expressionView;
        }

        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CalculatorControl control)
            {
                control.UpdateMode();
            }
        }
    }
}

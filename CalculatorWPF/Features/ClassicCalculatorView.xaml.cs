namespace System.Windows.Calculator
{
    using System.Windows;
    using System.Windows.Controls;

    using CalculatorWPF;

    /// <summary>
    /// Interaktionslogik für ClassicCalculatorView.xaml
    /// </summary>
    public partial class ClassicCalculatorView : UserControlBase
    {
        //private double _currentValue;
        //private double _storedValue;
        //private string _pendingOperator;

        private bool _isNewInput;
        //private bool _hasStoredValue;

        public ClassicCalculatorView() : base(typeof(ClassicCalculatorView))
        {
            this.InitializeComponent();
            WeakEventManager<UserControl, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
        }

        public CommandBase InputCommand { get; private set; }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.InputCommand = new CommandBase(commandParam => this.OnInput(commandParam), () => true);
            this.DataContext = this;
        }

        private void OnInput(object commandParam)
        {
            string key = commandParam as string;
            if (char.IsDigit(key[0]))
            {
                ProcessDigit(key);
                return;
            }

            switch (key)
            {
                case ",":
                    //ProcessDecimalSeparator();
                    break;

                case "±":
                    //ToggleSign();
                    break;

                case "⌫":
                    //Backspace();
                    break;

                case "C":
                    //ClearAll();
                    break;

                case "CE":
                    //ClearEntry();
                    break;

                case "+":
                case "-":
                case "×":
                case "÷":
                    //ProcessOperator(key);
                    break;

                case "=":
                    //Calculate();
                    break;
            }
        }

        private void ProcessDigit(string digit)
        {
            if (this._isNewInput)
            {
                this.DisplayText.Text = digit;
                this._isNewInput = false;
            }
            else
            {
                if (this.DisplayText.Text == "0")
                {
                    this.DisplayText.Text = digit;
                }
                else
                {
                    this.DisplayText.Text += digit;
                }
            }
        }
    }
}

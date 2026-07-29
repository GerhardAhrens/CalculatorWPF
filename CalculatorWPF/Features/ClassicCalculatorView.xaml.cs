namespace System.Windows.Calculator
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    using CalculatorWPF;

    /// <summary>
    /// Interaktionslogik für ClassicCalculatorView.xaml
    /// </summary>
    public partial class ClassicCalculatorView : UserControlBase
    {
        private double _currentValue;
        private double _storedValue;
        private string _pendingOperator;
        private bool _lastOperationWasEquals;
        private double _rightOperand;
        private double _leftOperand;
        private bool _isNewInput;
        private bool _hasStoredValue;

        public ClassicCalculatorView() : base(typeof(ClassicCalculatorView))
        {
            this.InitializeComponent();
            WeakEventManager<UserControl, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
        }

        public CommandBase InputCommand { get; private set; }

        public string DisplayText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        private double CurrentValue
        {
            get
            {
                if (double.TryParse(this.DisplayText, CultureInfo.CurrentCulture, out double value))
                {
                    return value;
                }

                return 0;
            }
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.InputCommand = new CommandBase(commandParam => this.OnInput(commandParam), () => true);
            this.DisplayText = "0";
            this.DataContext = this;
        }

        private void OnInput(object commandParam)
        {
            string key = commandParam as string;
            if (char.IsDigit(key[0]))
            {
                this.ProcessDigit(key);
                return;
            }

            switch (key)
            {
                case ",":
                    this.ProcessDecimalSeparator();
                    break;

                case "±":
                    this.ToggleSign();
                    break;

                case "⌫":
                    this.Backspace();
                    break;

                case "C":
                    this.ClearAll();
                    break;

                case "CE":
                    this.ClearEntry();
                    break;

                case "+":
                case "-":
                case "×":
                case "÷":
                    this.ProcessOperator(key);
                    break;

                case "=":
                    this.Calculate();
                    break;
            }
        }

        private void ProcessDigit(string digit)
        {
            if (this._isNewInput)
            {
                this.DisplayText = digit;
                this._isNewInput = false;
            }
            else
            {
                if (this.DisplayText == "0")
                {
                    this.DisplayText = digit;
                }
                else
                {
                    this.DisplayText += digit;
                }
            }
        }

        private void ProcessDecimalSeparator()
        {
            if (_isNewInput)
            {
                this.DisplayText = "0,";
                this._isNewInput = false;
                return;
            }

            if (this.DisplayText.Contains(',') == false)
            {
                this.DisplayText += ",";
            }
        }

        private void Backspace()
        {
            if (this._isNewInput)
                return;

            if (this.DisplayText.Length <= 1)
            {
                this.DisplayText = "0";
                this._isNewInput = true;
                return;
            }

            if (this.DisplayText.Length == 2 && this.DisplayText.StartsWith("-"))
            {
                this.DisplayText = "0";
                _isNewInput = true;
                return;
            }

            this.DisplayText = this.DisplayText[..^1];

            if (this.DisplayText == "-" || this.DisplayText == string.Empty)
            {
                this.DisplayText = "0";
                _isNewInput = true;
            }
        }

        private void ToggleSign()
        {
            if (this.DisplayText == "0")
                return;

            if (this.DisplayText.StartsWith("-"))
                this.DisplayText = this.DisplayText[1..];
            else
                this.DisplayText = "-" + this.DisplayText;
        }

        private void ClearEntry()
        {
            this.DisplayText = "0";
            this._isNewInput = true;
        }

        private void ClearAll()
        {
            this.DisplayText = "0";

            _leftOperand = 0;
            _rightOperand = 0;

            _pendingOperator = null;

            this._isNewInput = true;
        }

        private void ProcessOperator(string op)
        {
            if (this._hasStoredValue == false)
            {
                this._leftOperand = CurrentValue;
                this._hasStoredValue = true;
            }

            _pendingOperator = op;
            _isNewInput = true;
            _lastOperationWasEquals = false;
        }

        private void Calculate()
        {
            if (!_hasStoredValue || string.IsNullOrEmpty(_pendingOperator))
            {
                return;
            }

            double right = CurrentValue;
            double result = _leftOperand;

            switch (this._pendingOperator)
            {
                case "+":
                    result += right;
                    break;

                case "-":
                    result -= right;
                    break;

                case "×":
                    double tempResult = right * result;
                    result = tempResult;
                    break;

                case "÷":
                    result /= right;
                    break;
            }

            this.DisplayText = result.ToString();

            this._leftOperand = result;
            this._pendingOperator = null;
            this._isNewInput = true;
            this._lastOperationWasEquals = true;
        }
    }
}

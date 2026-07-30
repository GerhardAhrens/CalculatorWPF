namespace System.Windows.Calculator
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    using CalculatorWPF;

    /// <summary>
    /// Interaktionslogik für ClassicCalculatorView.xaml
    /// </summary>
    public partial class ClassicCalculatorView : UserControlBase
    {
        private const string ERRORTEXT = "Fehler";
        private string _pendingOperator;
        private bool _lastOperationWasEquals;
        private double _lastRightOperand;
        private string _lastOperator;
        //private double _rightOperand;
        private bool _hasError;
        private double _leftOperand;
        private bool _isNewInput;
        private bool _hasStoredValue;
        private double _memoryValue;
        private bool _memoryStored;


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

        public bool HasMemory { get { return this._memoryStored; } }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.InputCommand = new CommandBase(commandParam => this.OnProcessInput(commandParam), () => true);
            this.DisplayText = "0";
            this.DataContext = this;
        }

        private void OnProcessInput(object commandParam)
        {
            string key = commandParam as string;

            if (this._hasError == true)
            {
                switch (key)
                {
                    case "C":
                    case "CE":
                        this.ClearAll();
                        return;

                    case "MC":
                        this.MemoryClear();
                        return;

                    case "MR":
                        this.MemoryRecall();
                        _hasError = false;
                        return;

                    case "MS":
                        this.MemoryStore();
                        _hasError = false;
                        return;

                    case "M+":
                        this.MemoryAdd();
                        return;

                    case "M-":
                        this.MemorySubtract();
                        return;
                }

                if (key.Length == 1 && char.IsDigit(key[0]))
                {
                    this.ClearAll();
                }
                else
                {
                    return;
                }
            }

            if (key.Length == 1 && key[0] >= '0' && key[0] <= '9')
            {
                ProcessDigit(key);
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

                case "√x":
                    this.SquareRoot();
                    break;

                case "1/x":
                    this.Reciprocal();
                    break;


                case "x²":
                    Square();
                    break;

                case "%":
                    this.Percent();
                    break;

                case "=":
                    this.Calculate();
                    break;

                case "MC":
                    this.MemoryClear();
                    break;

                case "MR":
                    this.MemoryRecall();
                    break;

                case "MS":
                    this.MemoryStore();
                    break;

                case "M+":
                    this.MemoryAdd();
                    break;

                case "M-":
                    this.MemorySubtract();
                    break;
            }
        }

        private void ProcessDigit(string digit)
        {
            if (this._lastOperationWasEquals && string.IsNullOrEmpty(this._pendingOperator))
            {
                this._lastOperationWasEquals = false;
                this._lastOperator = null;
                this._lastRightOperand = 0;
                this._leftOperand = 0;
            }

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

        private void Calculate()
        {
            if (_hasError == true)
            {
                return;
            }

            // Wiederholtes "="
            if (string.IsNullOrEmpty(_pendingOperator))
            {
                if (string.IsNullOrEmpty(_lastOperator))
                    return;

                double result = ExecuteOperation(_leftOperand, _lastRightOperand,_lastOperator);

                _leftOperand = result;
                DisplayText = result.ToString();
                _isNewInput = true;

                return;
            }

            // Normale Berechnung
            double right = CurrentValue;

            _lastRightOperand = right;
            _lastOperator = _pendingOperator;

            double calcResult = ExecuteOperation(_leftOperand, right, _pendingOperator);

            _leftOperand = calcResult;

            DisplayText = calcResult.ToString();

            _pendingOperator = null;

            _isNewInput = true;
            _lastOperationWasEquals = true;
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
            double value = CurrentValue;

            if (value == 0)
            {
                return;
            }

            value = -value;

            this.DisplayText = value.ToString();
        }

        private void ClearEntry()
        {
            this.DisplayText = "0";
            this._isNewInput = true;

            if (this._lastOperationWasEquals)
            {
                this._lastOperationWasEquals = false;
                this._lastOperator = null;
                this._lastRightOperand = 0;
            }

            if (this._hasError == true)
            {
                this.ClearAll();
                return;
            }
        }

        private void ClearAll()
        {
            this.DisplayText = "0";

            this._leftOperand = 0;
            //this._rightOperand = 0;

            this._pendingOperator = null;

            this._hasStoredValue = false;
            this._isNewInput = true;

            this._lastOperationWasEquals = false;

            this._lastRightOperand = 0;
            this._lastOperator = null;
            this._hasError = false;
        }

        private void ProcessOperator(string op)
        {
            if (this._hasStoredValue == false)
            {
                this._leftOperand = CurrentValue;
                this._hasStoredValue = true;
            }
            else if (this._isNewInput == false)
            {
                this.ExecutePendingOperation(this._pendingOperator!);
            }

            this._pendingOperator = op;
            this._isNewInput = true;
            this._lastOperationWasEquals = false;
        }

        private double ExecuteOperation(double left,double right, string op)
        {
            switch (op)
            {
                case "+":
                    return left + right;

                case "-":
                    return left - right;

                case "×":
                    return left * right;

                case "÷":
                    if (right == 0)
                    {
                        this.SetError();
                        return 0;
                    }

                    return left / right;

                default:
                    throw new InvalidOperationException($"Unbekannter Operator '{op}'.");
            }
        }

        private bool ExecutePendingOperation(string op)
        {
            if (!_hasStoredValue)
                return false;

            double result = ExecuteOperation(_leftOperand, CurrentValue, op);

            _leftOperand = result;
            DisplayText = result.ToString();

            return true;
        }

        private void SquareRoot()
        {
            double value = CurrentValue;

            if (value < 0)
            {
                this.SetError();
                return;
            }

            value = Math.Sqrt(value);

            DisplayText = value.ToString(CultureInfo.CurrentCulture);

            // Die Wurzel ist jetzt der aktuelle Eingabewert.
            _isNewInput = false;
        }

        private void Square()
        {
            double value = CurrentValue;

            value *= value;

            DisplayText = value.ToString(CultureInfo.CurrentCulture);

            _isNewInput = false;
        }

        private void Reciprocal()
        {
            double value = CurrentValue;

            if (value == 0)
            {
                this.SetError();
                return;
            }

            value = 1 / value;

            this.DisplayText = value.ToString(CultureInfo.CurrentCulture);

            this._isNewInput = false;
        }

        private void SetError()
        {
            this.DisplayText = ERRORTEXT;

            this._hasError = true;

            this._leftOperand = 0;
            //this._rightOperand = 0;

            this._pendingOperator = null;

            this._hasStoredValue = false;
            this._isNewInput = true;

            this._lastOperationWasEquals = false;
            this._lastOperator = null;
            this._lastRightOperand = 0;
        }

        private void Percent()
        {
            if (!this._hasStoredValue || string.IsNullOrEmpty(this._pendingOperator))
                return;

            double value = CurrentValue;

            switch (this._pendingOperator)
            {
                case "+":
                case "-":
                    value = this._leftOperand * value / 100.0;
                    break;

                case "×":
                case "÷":
                    value /= 100.0;
                    break;
            }

            this.DisplayText = value.ToString(CultureInfo.CurrentCulture);
            this._isNewInput = false;
        }

        #region Memeory Verhalten
        private void MemoryStore()
        {
            this._memoryValue = CurrentValue;
            this._memoryStored = true;

            this.OnPropertyChanged(nameof(HasMemory));

            this._isNewInput = true;
        }

        private void MemoryRecall()
        {
            if (_memoryStored == false)
            {
                return;
            }

            this.DisplayText = _memoryValue.ToString(CultureInfo.CurrentCulture);

            this._isNewInput = true;
        }

        private void MemoryClear()
        {
            this._memoryValue = 0;
            this._memoryStored = false;

            this.OnPropertyChanged(nameof(HasMemory));
        }

        private void MemoryAdd()
        {
            this._memoryValue += CurrentValue;
            this._memoryStored = true;

            this.OnPropertyChanged(nameof(HasMemory));

            this._isNewInput = true;
        }

        private void MemorySubtract()
        {
            this._memoryValue -= CurrentValue;
            this._memoryStored = true;

            this.OnPropertyChanged(nameof(HasMemory));

            this._isNewInput = true;
        }
        #endregion Memeory Verhalten
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}

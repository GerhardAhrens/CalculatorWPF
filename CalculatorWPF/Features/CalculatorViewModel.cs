//-----------------------------------------------------------------------
// <copyright file="CalculatorViewModel .cs" company="Lifeprojects.de">
//     Class: CalculatorViewModel 
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>22.07.2026</date>
//
// <summary>
// Template für eine neue C# Standard-Klasse
// </summary>
//-----------------------------------------------------------------------

namespace System.Windows.Calculator
{
    using System;

    public class CalculatorViewModel : NotifyPropertyBase
    {
        private readonly CalculatorEngine _engine = new();

        public CalculatorViewModel ()
        {
            this.DisplayText = "0";
            this.ExpressionText = string.Empty;
            this.ErrorText = string.Empty;
        }

        public string DisplayText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value, this.SetValueMemory);
        }

        private void SetValueMemory(string arg1, string arg2)
        {
            this.MemoryValue = Convert.ToDouble(arg1);
        }

        public string ExpressionText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string ErrorText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public double Result
        {
            get => base.GetValue<double>();
            set => base.SetValue(value);
        }

        public double MemoryValue
        {
            get => _engine.MemoryValue;
            set
            {
                _engine.MemoryValue = value;
                base.OnPropertyChanged();
            }
        }

        public void EvaluateExpression()
        {
            ErrorText = string.Empty;

            try
            {
                this.Result = _engine.Evaluate(ExpressionText);
                this.DisplayText = Result.ToString();
            }
            catch (TokenizerException ex)
            {
                ErrorText = ex.Message;
            }
            catch (ParserException ex)
            {
                ErrorText = ex.Message;
            }
            catch (EvaluationException ex)
            {
                ErrorText = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
            }
        }
    }
}

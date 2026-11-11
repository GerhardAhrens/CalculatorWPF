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
    using System.Collections.ObjectModel;

    public class CalculatorViewModel : NotifyPropertyBase
    {
        private readonly CalculatorEngine _engine = new();

        public CalculatorViewModel ()
        {
            this.OpenFuncDialogF2Command = new CommandBase((param) => this.OnOpenFuncDialog(param));

            this.DisplayText = "0";
            this.ExpressionText = string.Empty;
            this.ErrorText = string.Empty;
        }

        public CommandBase OpenFuncDialogF2Command { get; private set; }


        public string DisplayText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
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
            get => base.GetValue<double>();
            set => base.SetValue(value);
        }

        public ObservableCollection<string> SearchSuggestions { get; }
    = new ObservableCollection<string>
    {
                "Berlin",
                "Bremen",
                "Dortmund",
                "Dresden",
                "Düsseldorf",
                "Frankfurt",
                "Hamburg",
                "Hannover",
                "Mannheim",
                "München",
                 "12345",
                "123456"
    };


        private void OnOpenFuncDialog(object commandParameter)
        {
        }

        public void EvaluateExpression()
        {
            ErrorText = string.Empty;

            try
            {
                CalculatorValue value = _engine.Evaluate(ExpressionText);

                DisplayText = value.ToString();

                if (value.IsNumber)
                {
                    this.Result = value;
                    this.MemoryValue = value;
                }
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

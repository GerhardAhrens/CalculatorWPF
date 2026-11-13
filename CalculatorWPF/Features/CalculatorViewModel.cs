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
    using System.Reflection;
    using System.Windows.Controls;

    public class CalculatorViewModel : NotifyPropertyBase
    {
        private readonly CalculatorEngine _engine = new();

        public CalculatorViewModel ()
        {
            this.OpenFuncDialogF2Command = new CommandBase((param) => this.OnOpenFuncDialog(param));
            this.FuncENTERCommand = new CommandBase(() => this.OnEnterFunc());

            this.DisplayText = "0";
            this.ExpressionText = string.Empty;
            this.ErrorText = string.Empty;

            var items = GetFormulaDescriptionData().OrderBy(item => item.Category).ToList();
            // Konstanten
            items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "PI",
                InsertText = "PI",
                Signature = "Number",
                Description = "Kreiszahl PI"
            });

            items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "E",
                InsertText = "E",
                Signature = "Number",
                Description = "Eulersche Zahl"
            });

            items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "π",
                InsertText = "π",
                Signature = "Number",
                Description = "Kreiszahl π"
            });

            items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "MWST",
                InsertText = "MWST",
                Signature = "Number",
                Description = "Mehrwertsteuer 19%"
            });

            items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "M",
                InsertText = "M"
            });

            this.SearchSuggestions = new ObservableCollection<string>();
            var textList = items.OrderBy(item => item.Category).Select(item => item.InsertText).ToList();
            foreach (string item in textList)
            {
                this.SearchSuggestions.Add(item);
            }
        }

        public CommandBase OpenFuncDialogF2Command { get; private set; }
        public CommandBase FuncENTERCommand { get; private set; }


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


        private void OnOpenFuncDialog(object commandParameter)
        {
            WindowTextBoxCommandParameter param = commandParameter as WindowTextBoxCommandParameter;

            FormulaSelectionWindow window = new FormulaSelectionWindow()
            {
                Owner = Window.GetWindow(this)
            };

            if (window.ShowDialog() != true)
            {
                return;
            }

            FormulaItem item = window.SelectedItem;

            if (item == null)
            {
                return;
            }

            this.ExpressionText = this.ExpressionText.Insert(this.ExpressionText.Length, item.InsertText);
        }

        private void OnEnterFunc()
        {
            this.EvaluateExpression();
        }

        public void EvaluateExpression()
        {
            this.ErrorText = string.Empty;

            try
            {
                CalculatorValue value = _engine.Evaluate(ExpressionText);

                this.DisplayText = value.ToString();

                if (value.IsNumber)
                {
                    this.Result = value;
                    this.MemoryValue = value;
                }
            }
            catch (TokenizerException ex)
            {
                this.ErrorText = ex.Message;
            }
            catch (ParserException ex)
            {
                this.ErrorText = ex.Message;
            }
            catch (EvaluationException ex)
            {
                this.ErrorText = ex.Message;
            }
            catch (Exception ex)
            {
                this.ErrorText = ex.Message;
            }
        }

        private List<FormulaItem> GetFormulaDescriptionData()
        {
            List<FormulaItem> items = new List<FormulaItem>();
            Assembly assembly = typeof(ICalculatorFunction).Assembly;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ICalculatorFunction).IsAssignableFrom(type) == false)
                {
                    continue;
                }

                FormulaItem item = GetFormulaItem(type);

                if (item != null)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        private static FormulaItem GetFormulaItem(Type type)
        {
            FormulaAttribute attribute = type.GetCustomAttribute<FormulaAttribute>();

            if (attribute == null)
                return null;

            return new FormulaItem
            {
                Category = attribute.Category,
                Name = attribute.Name,
                Signature = attribute.Signature,
                InsertText = attribute.InsertText,
                Example = attribute.Example,
                Description = attribute.Description
            };
        }

    }
}

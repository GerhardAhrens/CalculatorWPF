namespace System.Windows.Calculator
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaktionslogik für ExpressionCalculatorView.xaml
    /// </summary>
    public partial class ExpressionCalculatorView : UserControl
    {
        public ExpressionCalculatorView()
        {
            this.InitializeComponent();
        }

        private void ExpressionBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
            {
                return;
            }

            if (this.DataContext is CalculatorViewModel vm)
            {
                vm.EvaluateExpression();

                e.Handled = true;
            }
        }

        private void ExpressionBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.F2)
            {
                return;
            }

            FormulaSelectionWindow window = new FormulaSelectionWindow()
            {
                Owner = Window.GetWindow(this)
            };

            if (window.ShowDialog() != true)
                return;

            FormulaItem item = window.SelectedItem;

            if (item == null)
                return;

            int start = ExpressionBox.CaretIndex;

            ExpressionBox.Text = ExpressionBox.Text.Insert(start, item.InsertText);

            int pos = item.InsertText.IndexOf('(');

            ExpressionBox.CaretIndex = pos >= 0 ? start + pos + 1 : start + item.InsertText.Length;

            ExpressionBox.Focus();

            e.Handled = true;
        }
    }
}

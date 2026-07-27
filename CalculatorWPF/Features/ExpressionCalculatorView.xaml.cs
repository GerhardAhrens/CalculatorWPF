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

            if (DataContext is CalculatorViewModel vm)
            {
                vm.Calculate();

                e.Handled = true;
            }
        }
    }
}

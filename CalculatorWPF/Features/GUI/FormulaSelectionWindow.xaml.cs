namespace System.Windows.Calculator
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaktionslogik für FormulaSelectionWindow.xaml
    /// </summary>
    public partial class FormulaSelectionWindow : WindowBase
    {
        public FormulaSelectionWindow()
        {
            this.InitializeComponent();
            WeakEventManager<WindowBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);

            this.Items = new ObservableCollection<FormulaItem>();

            this.DataContext = this;
        }

        public ObservableCollection<FormulaItem> Items { get; }

        public FormulaItem SelectedItem
        {
            get => base.GetValue<FormulaItem>();
            set => base.SetValue(value);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Konstanten

            Items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "PI",
                InsertText = "PI"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "MWST",
                InsertText = "MWST"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Konstante",
                Name = "M",
                InsertText = "M"
            });

            // Funktionen

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Heute",
                Signature = "()",
                InsertText = "Heute()"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Date",
                Signature = "(Number; Number; Number)",
                InsertText = "Date(; ; )"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "DateDiff",
                Signature = "(String; DateTime; DateTime)",
                InsertText = "DateDiff(\"\"; ; )"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "KW",
                Signature = "(DateTime)",
                InsertText = "KW()"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Lookup",
                Signature = "(String; Key; String)",
                InsertText = "Lookup(\"\"; ; \"\")"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Format",
                Signature = "(Value; String)",
                InsertText = "Format(; \"\")"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "If",
                Signature = "(Boolean; Value; Value)",
                InsertText = "If(; ; )"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Sqrt",
                Signature = "(Number)",
                InsertText = "Sqrt()"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Rabatt",
                Signature = "(Number; Number)",
                InsertText = "Rabatt(; )"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Netto",
                Signature = "(Number;Number)",
                InsertText = "Netto(;)"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Brutto",
                Signature = "(Number;Number)",
                InsertText = "Brutto(;)"
            });
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogResult = true;
                return;
            }

            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                return;
            }

            base.OnPreviewKeyDown(e);
        }

        private void lvItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
        }
    }
}

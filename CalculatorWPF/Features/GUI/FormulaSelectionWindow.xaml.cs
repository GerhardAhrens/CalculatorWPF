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
                InsertText = "PI",
                Description = "Kreiszahl PI"
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

            Items.Add(new FormulaItem()
            {
                Category = "Datum",
                Name = "Heute",
                Signature = "()",
                InsertText = "Heute()",
                Description = "Gibt das Aktuelles Tagesdatum zurück"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Datum",
                Name = "Date",
                Signature = "(Number; Number; Number)",
                InsertText = "Date(; ; )",
                Example = $"date({DateTime.Now.Year}; {DateTime.Now.Month}; {DateTime.Now.Day})",
                Description = "Erzeugt zur internen Verwendung oder Berechung ein DateTime Datentyp"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Datum",
                Name = "DateDiff",
                Signature = "(String; DateTime; DateTime)",
                InsertText = "DateDiff(\"\"; ; )",
                Example = $"datediff(\"tag\";heute();Date({DateTime.Now.Year-1}; {DateTime.Now.Month}; {DateTime.Now.Day}))"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Datum",
                Name = "KW",
                Signature = "(DateTime)",
                InsertText = "KW()",
                Example = $"kw(Heute())"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Lookup",
                Signature = "(String; Key; String)",
                InsertText = "Lookup(\"\"; ; \"\")",
                Example = "Lookup(\"Artikel\";2001;\"B\") → Kugelschreiber oder Lookup(\"Artikel\";2001;\"C\") → 1,99"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "Format",
                Signature = "(Value; String)",
                InsertText = "Format(; \"\")",
                Example = "Format(1234.567;\"N2\") → 1.234,57 | Format(Heute();\"dd.MM.yyyy\") → 31.07.2026 | Format(Lookup(\"Artikel\";2001;\"C\");\"0.00 €\")"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "If",
                Signature = "(Boolean; Value; Value)",
                InsertText = "If(; ; )",
                Example = "If(1>0;\"Ja\";\"Nein\")"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Mathematik",
                Name = "Sqrt",
                Signature = "(Number)",
                InsertText = "Sqrt()",
                Example = "Sqrt(16) → 4"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Finanz",
                Name = "Rabatt",
                Signature = "(Number; Number)",
                InsertText = "Rabatt(; )",
                Example = "Rabatt(100;0.1) → 10"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Finanz",
                Name = "Netto",
                Signature = "(Number;Number)",
                InsertText = "Netto(;)",
                Example = "Netto(119;mwst) → 100"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Finanz",
                Name = "Brutto",
                Signature = "(Number;Number)",
                InsertText = "Brutto(;)",
                Example = "Brutto(100;mwst) → 119"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Umrechnung",
                Name = "KW2PS",
                Signature = "(Number)",
                InsertText = "KW2PS()",
                Example = "KW2PS(20) → 27"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Umrechnung",
                Name = "PS2KW",
                Signature = "(Number)",
                InsertText = "PS2KW()",
                Example = "PS2KW(27) → 20KW"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Geometrie",
                Name = "Umfang",
                Signature = "(Number)",
                InsertText = "Umfang()",
                Example = "Umfang(5) → 31,42"
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

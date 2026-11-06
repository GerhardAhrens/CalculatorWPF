namespace System.Windows.Calculator
{
    using System.Reflection;
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
        }

        public List<FormulaItem> Items { get; private set; }

        public FormulaItem SelectedItem
        {
            get => base.GetValue<FormulaItem>();
            set => base.SetValue(value);
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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

            this.Items = GetFormulaDescriptionData().OrderBy(item => item.Category).ToList(); 

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
                Category = "Funktion",
                Name = "Lookup",
                Signature = "(String; Key; String)",
                InsertText = "Lookup(\"\"; ; \"\")",
                Example = "Lookup(\"Artikel\";2001;\"B\") → Kugelschreiber oder Lookup(\"Artikel\";2001;\"C\") → 1,99"
            });

            Items.Add(new FormulaItem()
            {
                Category = "Funktion",
                Name = "If",
                Signature = "(Boolean; Value; Value)",
                InsertText = "If(; ; )",
                Example = "If(1>0;\"Ja\";\"Nein\")"
            });

            this.DataContext = this;

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

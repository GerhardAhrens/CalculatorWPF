namespace System.Windows.Calculator
{
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Documents;

    public class CalculatorEngine
    {
        public double MemoryValue { get; set; }

        private readonly Tokenizer _tokenizer = new();
        private readonly Parser _parser = new();
        private readonly ValueRegistry _valueRegistry = new();
        private readonly FunctionRegistry _functionRegistry;
        private readonly Evaluator _evaluator;

        public CalculatorEngine()
        {
            _functionRegistry = new FunctionRegistry();
            this._functionRegistry.Register(new SqrtFunction());
            this._functionRegistry.Register(new BruttoFunction());
            this._functionRegistry.Register(new NettoFunction());
            this._functionRegistry.Register(new RabattFunction());
            this._functionRegistry.Register(new HeuteFunction());
            this._functionRegistry.Register(new DateDiffFunction());
            this._functionRegistry.Register(new DateFunction());
            this._functionRegistry.Register(new FormatFunction());

            DataTable dtKunden = LadeKunden();
            DataTable dtArtikel = LadeArtikel();
            DataTableLookupProvider provider = new();
            provider.Add("Kunden", dtKunden, "A");
            provider.Add("Artikel", dtArtikel, "A");

            var lookupFunction = new LookupFunction
            {
                Provider = provider
            };

            this._functionRegistry.Register(lookupFunction);

            _evaluator = new Evaluator(this, _functionRegistry);

            _valueRegistry.Register("MwSt", () => 19);
            _valueRegistry.Register("PI", () => Math.PI);
            _valueRegistry.Register("E", () => Math.E);
            _valueRegistry.Register("M", () => MemoryValue);
        }

        public ILookupProvider LookupProvider { get; set; }

        public bool TryGetValue(string name, out double value)
        {
            return _valueRegistry.TryGetValue(name, out value);
        }

        public CalculatorValue Evaluate(string expression)
        {
            List<Token> tokens = _tokenizer.Tokenize(expression);

            ExpressionNode tree = _parser.Parse(tokens);

            return _evaluator.Evaluate(tree);
        }

        private static DataTable LadeKunden()
        {
            DataTable table = new("Kunden");

            table.Columns.Add("A", typeof(int));      // Key
            table.Columns.Add("B", typeof(string));   // Name
            table.Columns.Add("C", typeof(string));   // Ort

            table.PrimaryKey = new[] { table.Columns["A"] };

            table.Rows.Add(1001, "Müller", "Mannheim");
            table.Rows.Add(1002, "Meier", "Heidelberg");
            table.Rows.Add(1003, "Schmidt", "Karlsruhe");
            table.Rows.Add(1004, "Schulz", "Stuttgart");
            table.Rows.Add(1005, "Fischer", "Frankfurt");
            table.Rows.Add(1006, "Weber", "Darmstadt");
            table.Rows.Add(1007, "Wagner", "Mainz");
            table.Rows.Add(1008, "Becker", "Wiesbaden");
            table.Rows.Add(1009, "Hoffmann", "Koblenz");
            table.Rows.Add(1010, "Koch", "Speyer");

            return table;
        }

        private static DataTable LadeArtikel()
        {
            DataTable table = new("Artikel");

            table.Columns.Add("A", typeof(int));         // Key
            table.Columns.Add("B", typeof(string));      // Artikelname
            table.Columns.Add("C", typeof(decimal));     // Preis

            table.PrimaryKey = new[] { table.Columns["A"] };

            table.Rows.Add(2001, "Kugelschreiber", 1.99m);
            table.Rows.Add(2002, "Bleistift", 0.79m);
            table.Rows.Add(2003, "Radiergummi", 1.29m);
            table.Rows.Add(2004, "Notizblock", 3.49m);
            table.Rows.Add(2005, "Ordner", 4.99m);
            table.Rows.Add(2006, "Locher", 8.95m);
            table.Rows.Add(2007, "Tacker", 12.50m);
            table.Rows.Add(2008, "Lineal", 2.19m);
            table.Rows.Add(2009, "Schere", 6.75m);
            table.Rows.Add(2010, "Marker", 2.99m);

            return table;
        }
    }
}

namespace System.Windows.Calculator
{
    public sealed class MyLookupProvider : ILookupProvider
    {
        public CalculatorValue Lookup(string source, IReadOnlyList<CalculatorValue> keys, string field)
        {
            if (source.Equals("Kunden", StringComparison.OrdinalIgnoreCase))
            {
                int kundenNr = (int)keys[0].AsNumber();

                switch (kundenNr)
                {
                    case 1001:
                        if (field.Equals("Name", StringComparison.OrdinalIgnoreCase))
                            return "Müller";

                        if (field.Equals("Ort", StringComparison.OrdinalIgnoreCase))
                            return "Mannheim";
                        break;

                    case 1002:
                        if (field.Equals("Name", StringComparison.OrdinalIgnoreCase))
                            return "Meier";

                        if (field.Equals("Ort", StringComparison.OrdinalIgnoreCase))
                            return "Heidelberg";
                        break;
                }
            }

            if (source.Equals("Artikel", StringComparison.OrdinalIgnoreCase))
            {
                int artikelNr = (int)keys[0].AsNumber();

                if (artikelNr == 4711)
                {
                    if (field.Equals("Preis", StringComparison.OrdinalIgnoreCase))
                        return 19.95;

                    if (field.Equals("MwSt", StringComparison.OrdinalIgnoreCase))
                        return 19;
                }
            }

            return CalculatorValue.Null;
        }
    }
}

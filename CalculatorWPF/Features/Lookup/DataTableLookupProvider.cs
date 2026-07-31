namespace System.Windows.Calculator
{
    using System.Data;

    public sealed class DataTableLookupProvider : ILookupProvider
    {
        private readonly Dictionary<string, LookupTable> _tables =
            new(StringComparer.OrdinalIgnoreCase);

        public void Add(string source, DataTable table, string keyColumn)
        {
            _tables[source] = new LookupTable(table, keyColumn);
        }

        public bool Remove(string source)
        {
            return _tables.Remove(source);
        }

        public bool Contains(string source)
        {
            return _tables.ContainsKey(source);
        }

        public CalculatorValue Lookup(string source, IReadOnlyList<CalculatorValue> keys, string field)
        {
            if (_tables.TryGetValue(source, out LookupTable lookupTable) == false)
            {
                throw new EvaluationException($"Die Datenquelle '{source}' wurde nicht gefunden.");
            }

            if (keys.Count != 1)
            {
                throw new EvaluationException("Zurzeit wird genau ein Suchschlüssel unterstützt.");
            }

            if (lookupTable.Table.Columns.Contains(field) == false)
            {
                throw new EvaluationException($"Die Spalte '{field}' existiert nicht.");
            }

            string key = keys[0].ToString();

            DataRow row = lookupTable.Table.AsEnumerable()
                .FirstOrDefault(r => r[lookupTable.KeyColumn]?.ToString() == key);

            if (row == null)
            {
                return CalculatorValue.Null;
            }

            return CalculatorValue.From(row[field]);
        }
    }
}

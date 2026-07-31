namespace System.Windows.Calculator
{
    using System.Data;

    public sealed class DataTableLookupProvider : ILookupProvider
    {
        private readonly DataTable _table;
        private readonly string _keyColumn;

        public DataTableLookupProvider(DataTable table, string keyColumn)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _keyColumn = keyColumn ?? throw new ArgumentNullException(nameof(keyColumn));

            if (!_table.Columns.Contains(_keyColumn))
            {
                throw new ArgumentException($"Die Schlüsselspalte '{_keyColumn}' existiert nicht.");
            }
        }

        public CalculatorValue Lookup(string source, IReadOnlyList<CalculatorValue> keys, string field)
        {
            if (keys.Count != 1)
            {
                throw new EvaluationException("Es wird genau ein Suchschlüssel erwartet.");
            }

            if (!_table.Columns.Contains(field))
            {
                throw new EvaluationException($"Die Spalte '{field}' existiert nicht.");
            }

            object key = keys[0].Value;

            DataRow row = _table.AsEnumerable().FirstOrDefault(r => Equals(r[_keyColumn], key));

            if (row == null)
                return CalculatorValue.Null;

            return CalculatorValue.From(row[field]);
        }
    }
}

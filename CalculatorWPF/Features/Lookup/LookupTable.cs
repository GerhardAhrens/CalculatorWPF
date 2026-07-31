namespace System.Windows.Calculator
{
    using System.Data;

    public sealed class LookupTable
    {
        public LookupTable(DataTable table, string keyColumn)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            KeyColumn = keyColumn ?? throw new ArgumentNullException(nameof(keyColumn));

            if (Table.Columns.Contains(KeyColumn) == false)
            {
                throw new ArgumentException($"Die Schlüsselspalte '{KeyColumn}' existiert nicht.");
            }
        }

        public DataTable Table { get; }

        public string KeyColumn { get; }
    }
}

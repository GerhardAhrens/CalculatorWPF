namespace System.Windows.Calculator
{
    public class ValueRegistry
    {
        private readonly Dictionary<string, Func<double>> _values = new(StringComparer.OrdinalIgnoreCase);

        public void Register(string name, Func<double> getter)
        {
            _values[name] = getter;
        }

        public bool TryGetValue(string name, out double value)
        {
            if (_values.TryGetValue(name, out var getter))
            {
                value = getter();
                return true;
            }

            value = default;
            return false;
        }
    }
}

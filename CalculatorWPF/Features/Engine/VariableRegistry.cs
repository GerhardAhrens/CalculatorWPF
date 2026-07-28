namespace System.Windows.Calculator
{
    using System;
    using System.Collections.Generic;

    public class VariableRegistry
    {
        private readonly Dictionary<string, double> _variables = new(StringComparer.OrdinalIgnoreCase);

        public void Set(string name, double value)
        {
            _variables[name] = value;
        }

        public bool TryGetValue(string name, out double value)
        {
            return _variables.TryGetValue(name, out value);
        }
    }
}

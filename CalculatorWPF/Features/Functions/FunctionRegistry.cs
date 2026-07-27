namespace System.Windows.Calculator
{
    using System;
    using System.Collections.Generic;

    public class FunctionRegistry
    {
        private readonly Dictionary<string, ICalculatorFunction> _functions = new(StringComparer.OrdinalIgnoreCase);

        public void Register(ICalculatorFunction function)
        {
            _functions[function.Name] = function;
        }

        public bool TryGetFunction(string name, out ICalculatorFunction function)
        {
            return _functions.TryGetValue(name, out function);
        }
    }
}

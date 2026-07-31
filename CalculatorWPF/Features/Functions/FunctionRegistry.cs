namespace System.Windows.Calculator
{
    using System;
    using System.Collections.Generic;

    public class FunctionRegistry
    {
        private readonly Dictionary<string, ICalculatorFunction> _functions = new(StringComparer.OrdinalIgnoreCase);

        public IEnumerable<ICalculatorFunction> Functions => this._functions.Values;

        public void Register(ICalculatorFunction function)
        {
            this._functions[function.Name] = function;
        }

        public bool TryGetFunction(string name, out ICalculatorFunction function)
        {
            return this._functions.TryGetValue(name, out function);
        }
    }
}

namespace System.Windows.Calculator
{
    using System;

    public sealed class ValueDefinition
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public CalculatorValueType Type { get; init; }

        public Func<CalculatorValue> Getter { get; init; }
    }
}

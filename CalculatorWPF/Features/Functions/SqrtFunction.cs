namespace System.Windows.Calculator
{
    using System;

    public class SqrtFunction : ICalculatorFunction
    {
        public string Name => "sqrt";

        public int ParameterCount => 1;

        public double Execute(params double[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("sqrt erwartet genau einen Parameter.");
            }

            return Math.Sqrt(parameters[0]);
        }
    }
}

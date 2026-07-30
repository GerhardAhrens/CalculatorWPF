namespace System.Windows.Calculator
{
    using System;

    public class SqrtFunction : ICalculatorFunction
    {
        public string Name => "sqrt";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("sqrt erwartet genau einen Parameter.");
            }

            if (parameters[0].IsNumber == false)
            {
                throw new EvaluationException("Erster Parameter muss numerisch sein.");
            }

            return Math.Sqrt(parameters[0]);
        }
    }
}

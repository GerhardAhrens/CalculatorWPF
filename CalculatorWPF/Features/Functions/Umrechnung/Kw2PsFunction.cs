namespace System.Windows.Calculator
{
    using System;

    public sealed class Kw2PsFunction : ICalculatorFunction
    {
        public string Name => "KW2PS";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("KW2PS erwartet genau einen Parameter.");
            }

            if (parameters[0].IsNumber == false)
            {
                throw new EvaluationException("KW2PS erwartet einen numerischen Parameter.");
            }

            double kw = parameters[0].AsNumber();

            // 1 kW = 1,35962162 PS
            return kw * 1.35962162;
        }
    }
}

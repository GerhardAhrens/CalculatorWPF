namespace System.Windows.Calculator
{
    using System.Globalization;

    /// <summary>
    /// Die Klasse berechnet die Kalenderwoche (KW) für ein gegebenes Datum.
    /// </summary>
    /// <example>
    /// KW(Heute())
    /// KW(Date(2026;8;13))
    /// </example>
    public sealed class KwFunction : ICalculatorFunction
    {
        public string Name => "KW";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("KW erwartet genau einen Parameter.");
            }

            if (!parameters[0].IsDateTime)
            {
                throw new EvaluationException("KW erwartet einen DateTime-Parameter.");
            }

            DateTime date = parameters[0].AsDateTime();

            return ISOWeek.GetWeekOfYear(date);
        }
    }
}

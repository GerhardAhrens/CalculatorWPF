namespace System.Windows.Calculator
{
    /// <summary>
    /// Die Klasse berechnet die Differenz zwischen zwei Datumsangaben in verschiedenen Einheiten (Tage, Stunden, Minuten, Sekunden, Monate, Jahre).
    /// </summary>
    /// <example>
    /// DateDiff(Tag; Heute(); Date(2026;8;15))
    /// </example>
    public class DateDiffFunction : ICalculatorFunction
    {
        public string Name => "datediff";

        public int ParameterCount => 3;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != ParameterCount)
            {
                throw new ArgumentException($"DateDiff erwartet {ParameterCount} Parameter.");
            }

            if (parameters[0].IsString == false)
            {
                throw new EvaluationException("Erster Parameter muss ein Text sein.");
            }

            if (parameters[1].IsDateTime == false)
            {
                throw new EvaluationException("Zweiter Parameter muss ein Datum sein.");
            }

            if (parameters[2].IsDateTime == false)
            {
                throw new EvaluationException("Dritter Parameter muss ein Datum sein.");
            }

            string unit = parameters[0];
            DateTime start = parameters[1];
            DateTime end = parameters[2];

            switch (unit.ToLowerInvariant())
            {
                case "day":
                case "days":
                case "tag":
                case "tage":
                    return (end.Date - start.Date).Days;

                case "hour":
                case "hours":
                case "stunde":
                case "stunden":
                    return (end - start).TotalHours;

                case "minutes":
                case "minute":
                case "minuten":
                    return (end - start).TotalMinutes;

                case "second":
                case "seconds":
                case "sekunde":
                case "sekunden":
                    return (end - start).TotalSeconds;

                case "month":
                case "months":
                case "monat":
                case "monate":
                    return MonthsBetween(start, end);

                case "year":
                case "years":
                case "jahr":
                case "jahre":
                    return YearsBetween(start, end);

                default:
                    throw new EvaluationException($"Unbekannte Einheit '{unit}'.");
            }
        }

        private static int MonthsBetween(DateTime start, DateTime end)
        {
            int months = (end.Year - start.Year) * 12
                       + end.Month - start.Month;

            if (end.Day < start.Day)
                months--;

            return months;
        }

        private static int YearsBetween(DateTime start, DateTime end)
        {
            int years = end.Year - start.Year;

            if (end < start.AddYears(years))
                years--;

            return years;
        }
    }
}

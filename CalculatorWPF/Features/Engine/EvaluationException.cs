namespace System.Windows.Calculator
{
    using System;

    public class EvaluationException : Exception
    {
        public EvaluationException(string message) : base(message)
        {
        }
    }
}

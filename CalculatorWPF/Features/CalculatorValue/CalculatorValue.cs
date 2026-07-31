namespace System.Windows.Calculator
{
    public sealed class CalculatorValue
    {
        private readonly object _value;

        public CalculatorValueType Type { get; }

        public bool IsNull => Type == CalculatorValueType.Null;

        private CalculatorValue(CalculatorValueType type, object value)
        {
            Type = type;
            _value = value;
        }

        public bool IsNumber => Type == CalculatorValueType.Number;

        public bool IsString => Type == CalculatorValueType.String;

        public bool IsBoolean => Type == CalculatorValueType.Boolean;

        public bool IsDateTime => Type == CalculatorValueType.DateTime;

        public static CalculatorValue Null { get; } = new(CalculatorValueType.Null, null);

        public static CalculatorValue From(double value) => new(CalculatorValueType.Number, value);

        public static CalculatorValue From(string value) => new(CalculatorValueType.String, value);

        public static CalculatorValue From(bool value) => new(CalculatorValueType.Boolean, value);

        public static CalculatorValue From(DateTime value) => new(CalculatorValueType.DateTime, value);

        public static CalculatorValue From(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return Null;
            }

            return value switch
            {
                CalculatorValue cv => cv,

                string s => From(s),

                int i => From((double)i),

                long l => From((double)l),

                short s => From((double)s),

                float f => From((double)f),

                double d => From(d),

                decimal m => From((double)m),

                bool b => From(b),

                DateTime dt => From(dt),

                _ => From(value.ToString())
            };
        }

        public object ToObject()
        {
            return _value;
        }

        public double AsNumber()
        {
            if (Type != CalculatorValueType.Number)
            {
                throw new InvalidOperationException($"Wert ist kein Number sondern {Type}.");
            }

            return (double)_value!;
        }

        public string AsString()
        {
            if (Type != CalculatorValueType.String)
            {
                throw new InvalidOperationException($"Wert ist kein String sondern {Type}.");
            }

            return (string)_value!;
        }

        public bool AsBoolean()
        {
            if (Type != CalculatorValueType.Boolean)
            {
                throw new InvalidOperationException($"Wert ist kein Boolean sondern {Type}.");
            }

            return (bool)_value!;
        }

        public DateTime AsDateTime()
        {
            if (Type != CalculatorValueType.DateTime)
            {
                throw new InvalidOperationException($"Wert ist kein DateTime sondern {Type}.");
            }

            return (DateTime)_value!;
        }

        public static implicit operator CalculatorValue(double value) => From(value);

        public static implicit operator CalculatorValue(string value)  => From(value);

        public static implicit operator CalculatorValue(bool value) => From(value);

        public static implicit operator CalculatorValue(DateTime value) => From(value);

        public static implicit operator double(CalculatorValue value) => value.AsNumber();

        public static implicit operator string(CalculatorValue value) => value.AsString();

        public static implicit operator bool(CalculatorValue value) => value.AsBoolean();

        public static implicit operator DateTime(CalculatorValue value) => value.AsDateTime();

        public override string ToString() => _value?.ToString() ?? String.Empty;

        public string ToDisplayString(string format = null)
        {
            return Type switch
            {
                CalculatorValueType.Null => String.Empty,

                CalculatorValueType.Number => format == null ? AsNumber().ToString() : AsNumber().ToString(format),

                CalculatorValueType.String => AsString(),

                CalculatorValueType.Boolean => AsBoolean().ToString(),

                CalculatorValueType.DateTime => format == null ? AsDateTime().ToString() : AsDateTime().ToString(format), _ => String.Empty
            };
        }
    }
}

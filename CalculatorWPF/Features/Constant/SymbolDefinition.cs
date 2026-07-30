namespace System.Windows.Calculator
{
    public abstract class SymbolDefinition
    {
        public string Name { get; }

        protected SymbolDefinition(string name)
        {
            Name = name;
        }
    }
}

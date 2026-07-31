namespace System.Windows.Calculator
{
    public abstract class FunctionDefinition
    {
        public string Name { get; }

        protected FunctionDefinition(string name)
        {
            this.Name = name;
        }
    }
}

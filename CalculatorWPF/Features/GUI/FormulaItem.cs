namespace System.Windows.Calculator
{
    public class FormulaItem
    {
        public string Category { get; set; }

        public string Name { get; set; }

        public string Signature { get; set; }

        public string InsertText { get; set; }

        public string Example { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

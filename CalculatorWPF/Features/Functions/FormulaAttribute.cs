namespace System.Windows.Calculator
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class FormulaAttribute : Attribute
    {
        public string Category { get; }

        public string Name { get; }

        public string Signature { get; }

        public string InsertText { get; }

        public string Example { get; }

        public string Description { get; }

        public FormulaAttribute(
            string category,
            string name,
            string signature,
            string insertText,
            string example = "",
            string description = "")
        {
            Category = category;
            Name = name;
            Signature = signature;
            InsertText = insertText;
            Example = example;
            Description = description;
        }
    }
}

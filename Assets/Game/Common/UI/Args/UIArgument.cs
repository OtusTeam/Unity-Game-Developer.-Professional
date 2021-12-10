namespace Prototype
{
    public readonly struct UIArgument
    {
        public UIArgumentName Name { get; }

        public object Value { get; }

        public UIArgument(UIArgumentName name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
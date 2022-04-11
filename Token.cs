namespace CompilerFinal
{
    public class Token
    {
        public TipoToken Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
        public Token(TipoToken kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
    }
}

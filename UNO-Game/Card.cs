namespace UNO_Game
{
    class Card
    {
        public ColorType Color;

        public ValueType Value;

        public Card(ValueType value, ColorType color)
        {
            this.Value = value;
            this.Color = color;
        }

        public override string ToString()
        {
            return $"{Color} {Value}";
        }
    }
    public enum ColorType
    {
        Green,
        Yellow,
        Blue,
        Red,
        NA
    }
    public enum ValueType
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Stop,
        Reverse,
        Plus2,
        ChangeColor,
        Plus4
    }


}
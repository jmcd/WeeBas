namespace WeeBas.Grammar
{
    public class Digit
    {
        public readonly string Body;

        private Digit(string body)
        {
            Body = body;
        }
        
        public static Digit Parse(Input input)
        {
            var s = input.Pop("0", "1", "2", "3", "4", "5", "6", "7", "8", "9");
            return s != null ? new Digit(s) : null;
        }
    }
}
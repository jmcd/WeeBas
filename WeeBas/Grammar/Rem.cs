using System.IO;

namespace WeeBas.Grammar
{
    public class Rem : ICommand
    {
        public const string Keyword = "rem";

        public static Rem Parse(Input input, TextWriter output)
        {
            var rem = input.ArglessSymbol<Rem>(Keyword);
            input.FastForward();
            return rem;
        }

        public void ExecuteIn(Vm vm) {}
    }
}
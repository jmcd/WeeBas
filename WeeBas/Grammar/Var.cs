using System.IO;

namespace WeeBas.Grammar
{
    public class Var
    {
        public readonly string Name;

        private Var(string name)
        {
            Name = name;
        }

        public static Var Parse(Input input, TextWriter output)
        {
            var s = input.Pop("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
            return s != null ? new Var(s) : null;
        }

        public int EvalInt(Vm vm)
        {
            return vm[Name];
        }
    }
}
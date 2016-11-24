using System.IO;

namespace WeeBas.Grammar
{
    public class TermCont
    {
        public readonly string Op;
        public readonly Factor Factor;

        private TermCont(string op, Factor factor)
        {
            Op = op;
            Factor = factor;
        }

        public static TermCont Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                var op = input.Pop("*", "/");
                if (op == null)
                {
                    return null;
                }
                var factor = Factor.Parse(input, output);
                return factor == null ? null : new TermCont(op, factor);
            });
        }
    }
}
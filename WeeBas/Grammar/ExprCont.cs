using System.IO;

namespace WeeBas.Grammar
{
    public class ExprCont
    {
        public readonly string Op;
        public readonly Term Term;

        private ExprCont(string op, Term term)
        {
            this.Op = op;
            this.Term = term;
        }

        public static ExprCont Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                var op = input.Pop("+", "-");
                if (op == null)
                {
                    return null;
                }
                var term = Term.Parse(input, output);
                return term == null ? null : new ExprCont(op, term);
            });
        }
    }
}
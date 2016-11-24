using System.IO;

namespace WeeBas.Grammar
{
    public class Rnd
    {
        private readonly Expr expr;

        private Rnd(Expr expr)
        {
            this.expr = expr;
        }

        private const string Keyword = "!rnd";

        public static Rnd Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                if (input.Pop(Keyword) == null)
                {
                    return null;
                }
                var expr = Expr.Parse(input, output);
                if (expr == null)
                {
                    output.WriteLine(MessageFormatter.Expected("expression"));
                    return null;
                }
                return new Rnd(expr);
            });
        }

        public int EvalInt(Vm vm)
        {
            var ulim = expr.EvalInt(vm);
            return vm.Random(ulim);
        }
    }
}
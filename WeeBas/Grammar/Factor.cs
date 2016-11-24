using System.IO;

namespace WeeBas.Grammar
{
    public class Factor
    {
        private readonly Expr expr;
        private readonly Number number;
        private readonly Var var;
        private readonly Rnd rnd;

        private Factor(Var var)
        {
            this.var = var;
        }

        private Factor(Number number)
        {
            this.number = number;
        }

        private Factor(Expr expr)
        {
            this.expr = expr;
        }

        private Factor(Rnd rnd)
        {
            this.rnd = rnd;
        }

        private static int? indexOnPreviousVisit;

        private static bool Dejavu(int currentIndex)
        {
            return indexOnPreviousVisit == currentIndex;
        }

        public static Factor Parse(Input input, TextWriter output)
        {
            if (Dejavu(input.Index))
            {
                return null;
            }

            indexOnPreviousVisit = input.Index;

            var var = Var.Parse(input, output);

            if (var != null)
            {
                indexOnPreviousVisit = null;
                return new Factor(var);
            }

            var number = Number.Parse(input);
            if (number != null)
            {
                indexOnPreviousVisit = null;
                return new Factor(number);
            }

            var expr = Expr.Parse(input, output);
            if (expr != null)
            {
                indexOnPreviousVisit = null;
                return new Factor(expr);
            }

            var rnd = Rnd.Parse(input, output);
            if (rnd != null)
            {
                indexOnPreviousVisit = null;
                return new Factor(rnd);
            }

            return null;
        }

        public int EvalInt(Vm vm)
        {
            return var?.EvalInt(vm) ??
                   number?.EvalInt(vm) ??
                   expr?.EvalInt(vm) ??
                   rnd.EvalInt(vm);
        }
    }
}
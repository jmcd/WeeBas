using System.Collections.Generic;
using System.IO;

namespace WeeBas.Grammar
{
    public class Expr
    {
        private readonly string sign;
        private readonly Term term;
        private readonly IList<ExprCont> cont;

        private Expr(string sign, Term term, IList<ExprCont> cont)
        {
            this.sign = sign;
            this.term = term;
            this.cont = cont;
        }

        public static Expr Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                var sign = input.Pop("+", "-");
                var term = Term.Parse(input, output);
                return term == null ? null : new Expr(sign, term, input.UntilNull(output, ExprCont.Parse));
            });
        }

        public string EvalString(Vm vm)
        {
            return EvalInt(vm).ToString();
        }

        public int EvalInt(Vm vm)
        {
            var m = sign == "-" ? -1 : 1;
            var lhs = m*term.EvalInt(vm);
            foreach (var exprCont in cont)
            {
                var intOp = Ops.IntOps[exprCont.Op];
                var rhs = exprCont.Term.EvalInt(vm);
                lhs = intOp(lhs, rhs);
            }
            return lhs;
        }
    }
}
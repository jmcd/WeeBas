using System.Collections.Generic;
using System.IO;

namespace WeeBas.Grammar
{
    public class Term
    {
        private readonly Factor factor;
        private readonly IList<TermCont> cont;

        private Term(Factor factor, IList<TermCont> cont)
        {
            this.factor = factor;
            this.cont = cont;
        }

        public static Term Parse(Input input, TextWriter output)
        {
            var factor = Factor.Parse(input, output);
            return factor == null ? null : new Term(factor, input.UntilNull(output, TermCont.Parse));
        }

        public int EvalInt(Vm vm)
        {
            var lhs = factor.EvalInt(vm);
            foreach (var termCont in cont)
            {
                var intOp = Ops.IntOps[termCont.Op];
                var rhs = termCont.Factor.EvalInt(vm);
                lhs = intOp(lhs, rhs);
            }
            return lhs;
        }
    }
}
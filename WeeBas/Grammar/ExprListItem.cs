using System.IO;

namespace WeeBas.Grammar
{
    public class ExprListItem
    {
        private readonly Expr expr;
        private readonly Str str;

        private ExprListItem(Str str)
        {
            this.str = str;
        }

        private ExprListItem(Expr expr)
        {
            this.expr = expr;
        }

        public static ExprListItem Parse(Input input, TextWriter output)
        {
            var str = Str.Parse(input);
            if (str != null)
            {
                return new ExprListItem(str);
            }

            var expr = Expr.Parse(input, output);
            return expr != null ? new ExprListItem(expr) : null;
        }

        public string EvalString(Vm vm)
        {
            return str?.EvalString(vm) ?? expr?.EvalString(vm);
        }
    }
}
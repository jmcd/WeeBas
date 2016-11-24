using System.IO;

namespace WeeBas.Grammar
{
    public class Let : ICommand
    {
        private readonly Var var;
        private readonly Expr expr;

        private Let(Var var, Expr expr)
        {
            this.var = var;
            this.expr = expr;
        }

        public const string Keyword = "let";

        public static Let Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                if (input.Pop(Keyword) == null)
                {
                    return null;
                }
                var var = Var.Parse(input, output);
                if (var == null)
                {
                    output.WriteLine(MessageFormatter.Expected("var"));
                    return null;
                }
                if (input.Pop("=") == null)
                {
                    output.WriteLine(MessageFormatter.Expected("="));
                    return null;
                }
                var expr = Expr.Parse(input, output);
                if (expr == null)
                {
                    output.WriteLine(MessageFormatter.Expected("expression"));
                    return null;
                }
                return new Let(var, expr);
            });
        }

        public void ExecuteIn(Vm vm)
        {
            vm[var.Name] = expr.EvalInt(vm);
        }
    }
}
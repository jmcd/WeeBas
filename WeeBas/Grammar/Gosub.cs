using System.IO;

namespace WeeBas.Grammar
{
    public class Gosub : ICommand
    {
        private readonly Expr expr;

        private Gosub(Expr expr)
        {
            this.expr = expr;
        }

        public const string Keyword = "gosub";

        public static Gosub Parse(Input input, TextWriter output)
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
                return new Gosub(expr);
            });
        }

        public void ExecuteIn(Vm vm)
        {
            var lineNumber = expr.EvalInt(vm);
            vm.Gosub(lineNumber);
        }
    }
}
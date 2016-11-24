using System.IO;

namespace WeeBas.Grammar
{
    public class Goto : ICommand
    {
        private readonly Expr expr;

        private Goto(Expr expr)
        {
            this.expr = expr;
        }

        public const string Keyword = "goto";

        public static Goto Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                var cmd = input.Pop(Keyword);
                if (cmd == null)
                {
                    return null;
                }
                var expr = Expr.Parse(input, output);
                if (expr == null)
                {
                    output.WriteLine(MessageFormatter.Expected("expression"));
                    return null;
                }
                return new Goto(expr);
            });
        }

        public void ExecuteIn(Vm vm)
        {
            var lineNumber = expr.EvalInt(vm);
            vm.Goto(lineNumber);
        }
    }
}
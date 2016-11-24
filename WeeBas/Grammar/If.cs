using System.IO;

namespace WeeBas.Grammar
{
    public class If : ICommand
    {
        private readonly Expr lhExpr;
        private readonly Relop relop;
        private readonly Expr rhExpr;
        private readonly Statement statement;

        private If(Expr lhExpr, Relop relop, Expr rhExpr, Statement statement)
        {
            this.lhExpr = lhExpr;
            this.relop = relop;
            this.rhExpr = rhExpr;
            this.statement = statement;
        }

        public const string KeywordIf = "if";
        private const string KeywordThen = "then";

        public static If Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                var cmd = input.Pop(KeywordIf);
                if (cmd == null)
                {
                    return null;
                }
                var lhExpr = Expr.Parse(input, output);
                if (lhExpr == null)
                {
                    output.WriteLine(MessageFormatter.Expected("expression"));
                    return null;
                }
                var relop = Relop.Parse(input);
                if (relop == null)
                {
                    output.WriteLine(MessageFormatter.Expected(Relop.Strings));
                    return null;
                }
                var rhExpr = Expr.Parse(input, output);
                if (rhExpr == null)
                {
                    output.WriteLine(MessageFormatter.Expected("expression"));
                    return null;
                }
                var then = input.Pop(KeywordThen);
                if (then == null)
                {
                    output.WriteLine(MessageFormatter.Expected(KeywordThen));
                    return null;
                }
                var statement = Statement.Parse(input, output);
                if (statement == null)
                {
                    output.WriteLine(MessageFormatter.Expected("statement"));
                    return null;
                }
                return new If(lhExpr, relop, rhExpr, statement);
            });
        }

        public void ExecuteIn(Vm vm)
        {
            var lhs = lhExpr.EvalInt(vm);
            var relOp = Ops.RelOps[relop.s];
            var rhs = rhExpr.EvalInt(vm);
            var pass = relOp(lhs, rhs);
            if (pass)
            {
                statement.Command.ExecuteIn(vm);
            }
        }
    }
}
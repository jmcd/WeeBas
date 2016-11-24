using System.IO;

namespace WeeBas.Grammar
{
    public class Print : ICommand
    {
        private readonly ExprList exprList;

        private Print(ExprList exprList)
        {
            this.exprList = exprList;
        }

        public const string Keyword = "print";

        public static Print Parse(Input input, TextWriter output)
        {
            var exprList = input.ParseKeywordThenExprList(Keyword, output);
            return exprList == null ? null : new Print(exprList);
        }

        public void ExecuteIn(Vm vm)
        {
            var s = exprList.EvalString(vm);
            vm.Write(s);
        }
    }
}
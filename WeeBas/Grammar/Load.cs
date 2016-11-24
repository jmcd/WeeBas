using System.IO;

namespace WeeBas.Grammar
{
    public class Load : ICommand
    {
        private readonly ExprList exprList;

        private Load(ExprList exprList)
        {
            this.exprList = exprList;
        }

        public const string Keyword = "load";

        public static Load Parse(Input input, TextWriter output)
        {
            var exprList = input.ParseKeywordThenExprList(Keyword, output);
            return exprList == null ? null : new Load(exprList);
        }

        public void ExecuteIn(Vm vm)
        {
            var s = exprList.EvalString(vm);
            vm.Load(s);
        }
    }
}
using System.IO;

namespace WeeBas.Grammar
{
    public class Save : ICommand
    {
        private readonly ExprList exprList;

        private Save(ExprList exprList)
        {
            this.exprList = exprList;
        }

        public const string Keyword = "save";

        public static Save Parse(Input input, TextWriter output)
        {
            var exprList = input.ParseKeywordThenExprList(Keyword, output);
            return exprList == null ? null : new Save(exprList);
        }

        public void ExecuteIn(Vm vm)
        {
            var s = exprList.EvalString(vm);
            vm.Save(s);
        }
    }
}
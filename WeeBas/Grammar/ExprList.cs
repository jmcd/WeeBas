using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeeBas.Grammar
{
    public class ExprList
    {
        private readonly IList<ExprListItem> exprListItems;

        private ExprList(IList<ExprListItem> exprListItems)
        {
            this.exprListItems = exprListItems;
        }

        public static ExprList Parse(Input input, TextWriter output)
        {
            var exprListItems = input.UntilNull(output, ExprListItem.Parse);
            return exprListItems.Any() ? new ExprList(exprListItems) : null;
        }

        public string EvalString(Vm vm)
        {
            return string.Join("", exprListItems.Select(li => li.EvalString(vm)));
        }
    }
}
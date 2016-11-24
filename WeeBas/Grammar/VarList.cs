using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeeBas.Grammar
{
    public class VarList
    {
        public readonly IList<Var> Vars;

        private VarList(IList<Var> vars)
        {
            Vars = vars;
        }

        public static VarList Parse(Input input, TextWriter output)
        {
            var vars = input.UntilNull(output, Var.Parse);
            return vars.Any() ? new VarList(vars) : null;
        }
    }
}
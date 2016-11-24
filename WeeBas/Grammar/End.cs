using System.IO;

namespace WeeBas.Grammar
{
    public class End : ICommand
    {
        public const string Keyword = "end";

        public static End Parse(Input input, TextWriter output)
        {
            return input.ArglessSymbol<End>(Keyword);
        }

        public void ExecuteIn(Vm vm)
        {
            vm.End();
        }
    }
}
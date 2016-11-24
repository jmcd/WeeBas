using System.IO;

namespace WeeBas.Grammar
{
    public class ListCmd : ICommand
    {
        public const string Keyword = "list";

        public static ListCmd Parse(Input input, TextWriter output)
        {
            return input.ArglessSymbol<ListCmd>(Keyword);
        }

        public void ExecuteIn(Vm vm)
        {
            vm.List();
        }
    }
}
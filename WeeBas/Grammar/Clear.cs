using System.IO;

namespace WeeBas.Grammar
{
    public class Clear : ICommand
    {
        public const string Keyword = "clear";

        public static Clear Parse(Input input, TextWriter output)
        {
            return input.ArglessSymbol<Clear>(Keyword);
        }

        public void ExecuteIn(Vm vm)
        {
            vm.Clear();
        }
    }
}
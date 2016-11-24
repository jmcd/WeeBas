using System.IO;

namespace WeeBas.Grammar
{
    public class Return : ICommand
    {
        public const string Keyword = "return";

        public static Return Parse(Input input, TextWriter output)
        {
            return input.ArglessSymbol<Return>(Keyword);
        }

        public void ExecuteIn(Vm vm)
        {
            vm.Return();
        }
    }
}
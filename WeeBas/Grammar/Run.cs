using System.IO;

namespace WeeBas.Grammar
{
    public class Run : ICommand
    {
        public const string Keyword = "run";

        public static Run Parse(Input input, TextWriter output)
        {
            return input.ArglessSymbol<Run>(Keyword);
        }

        public void ExecuteIn(Vm vm)
        {
            vm.Run();
        }
    }
}
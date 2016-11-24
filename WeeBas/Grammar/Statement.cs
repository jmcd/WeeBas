using System.Collections.Generic;
using System.IO;

namespace WeeBas.Grammar
{
    public class Statement
    {
        public readonly ICommand Command;

        private delegate ICommand CommandParser(Input input, TextWriter output);

        private static readonly IList<CommandParser> Parsers = new List<CommandParser>
        {
            Print.Parse, If.Parse, Goto.Parse, InputCmd.Parse, Let.Parse, Gosub.Parse, Return.Parse, Clear.Parse, ListCmd.Parse, Run.Parse, End.Parse, Save.Parse, Load.Parse, Rem.Parse,
        };

        private Statement(ICommand command)
        {
            Command = command;
        }

        public static Statement Parse(Input input, TextWriter output)
        {
            foreach (var commandParser in Parsers)
            {
                var command = commandParser(input, output);
                if (command != null)
                {
                    return new Statement(command);
                }
            }
            output.WriteLine(MessageFormatter.Expected(Print.Keyword, If.KeywordIf, Goto.Keyword, InputCmd.Keyword, Let.Keyword, Gosub.Keyword, 
                Return.Keyword, Clear.Keyword, ListCmd.Keyword, Run.Keyword, End.Keyword, Save.Keyword, Load.Keyword, Rem.Keyword));
            return null;
        }
    }
}
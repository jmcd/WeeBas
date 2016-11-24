using System;
using System.IO;

namespace WeeBas.Grammar
{
    public class Line
    {
        public readonly Number Number;
        public readonly Statement Statement;
        private readonly string rawUserInput;

        private Line(Number number, Statement statement, string rawUserInput)
        {
            Number = number;
            Statement = statement;
            this.rawUserInput = rawUserInput;
        }

        public static Line Parse(Input input, TextWriter output)
        {
            return input.RewindOnNull(() =>
            {
                var number = Number.Parse(input);
                var statement = Statement.Parse(input, output);
                if (statement == null)
                {
                    output.WriteLine(MessageFormatter.Expected("statement"));
                    return null;
                }
                var remainder = input.Pop(length: Int32.MaxValue).Trim();
                
                if (remainder.Length != 0)
                {
                    output.WriteLine(MessageFormatter.Expected($"eol (line was \"{input.Body}\")"));
                    return null;
                }
                return new Line(number, statement, input.Body);
            });
        }

        public override string ToString()
        {
            return rawUserInput;
        }
    }
}
using System;

namespace WeeBas.Grammar
{
    public class Str
    {
        private readonly string body;

        private Str(string body)
        {
            this.body = body;
        }

        public static Str Parse(Input input)
        {
            return input.RewindOnNull(() =>
            {
                var opening = input.Pop("\"");
                if (opening == null)
                {
                    return null;
                }
                var s = "";
                var bodyChar = default(String);
                while ((bodyChar = input.Pop()) != "\"")
                {
                    s += bodyChar;
                }
                return new Str(s);
            });
        }

        public string EvalString(Vm vm)
        {
            return body;
        }
    }
}
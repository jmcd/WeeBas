using System.Collections.Generic;
using System.Linq;

namespace WeeBas.Grammar
{
    public class Number
    {
        public readonly List<Digit> Digits;

        private Number(List<Digit> digits)
        {
            Digits = digits;
        }

        public static Number Parse(Input input)
        {
            var digits = new List<Digit>();
            var digit = default(Digit);
            while ((digit = Digit.Parse(input)) != null)
            {
                digits.Add(digit);
            }
            return digits.Any() ? new Number(digits) : null;
        }

        public int EvalInt(Vm vm)
        {
            return int.Parse(string.Join("", Digits.Select(d => d.Body)));
        }
    }
}
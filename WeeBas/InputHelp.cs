using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework.Interfaces;
using WeeBas.Grammar;

namespace WeeBas
{
    public static class InputHelp
    {
        private static string PopSingle(this Input @this, string s)
        {
            var candidate = @this.Peek(s.Length);
            return candidate.Equals(s, StringComparison.OrdinalIgnoreCase) ? @this.Pop(s.Length) : null;
        }

        public static string Pop(this Input @this, params string[] choices)
        {
            return @this.RewindOnNull(() =>
            {
                while (@this.Peek() == " ")
                {
                    @this.Pop();
                }

                foreach (var choice in choices)
                {
                    var candidate = @this.PopSingle(choice);
                    if (candidate != null)
                    {
                        return candidate;
                    }
                }
                return null;
            });
        }

        public static T RewindOnNull<T>(this Input @this, Func<T> fn)
        {
            @this.Mark();
            var result = fn();
            if (result == null)
            {
                @this.RewindToMark();
            }
            else
            {
                @this.CommitMark();
            }
            return result;
        }

        public static IList<T> UntilNull<T>(this Input input, TextWriter output, Func<Input, TextWriter, T> fn)
        {
            var items = new List<T>();
            var item = default(T);
            while ((item = fn(input, output)) != null)
            {
                items.Add(item);
            }
            return items;
        }


        public static T ArglessSymbol<T>(this Input @this, string keyword) where T : new()
        {
            return @this.Pop(keyword) == null ? default(T) : new T();
        }

        public static ExprList ParseKeywordThenExprList(this Input @this, string keyword, TextWriter output)
        {
            var el = @this.RewindOnNull(() =>
            {
                var cmd = @this.Pop(keyword);
                if (cmd == null)
                {
                    return null;
                }
                var exprList = ExprList.Parse(@this, output);
                if (exprList == null)
                {
                    output.WriteLine(MessageFormatter.Expected("expression-list"));
                    return null;
                }
                return exprList;
            });
            return el;
        }
    }
}
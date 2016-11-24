using System.IO;

namespace WeeBas
{
    public static class MessageFormatter
    {
        private static string SyntaxError(string message)
        {
            return $"syntax error, {message}";
        }

        public static string Expected(string what)
        {
            return SyntaxError($"expected {what}");
        }

        public static string Expected(params string[] what)
        {
            return Expected($"one of: {string.Join(", ", what)}");
        }

        public static string Error(string message, int? line)
        {
            return line.HasValue ? $"error at line {line.Value}, {message}" : $"error, {message}";
        }

    }
//
//    public class Output
//    {
//        private readonly TextWriter writer;
//
//        public Output(TextWriter writer)
//        {
//            this.writer = writer;
//        }
//
////        private void SyntaxError(string message)
////        {
////            writer.WriteLine($"syntax error, {message}");
////        }
////
////        public void Expected(string what)
////        {
////            SyntaxError($"expected {what}");
////        }
////
////        public void Expected(params string[] what)
////        {
////            Expected($"one of: {string.Join(", ", what)}");
////        }
////        
////        public void Error(string message)
////        {
////            writer.WriteLine($"error, {message}");
////        }
//
//        public void WriteLine(string message)
//        {
//            writer.WriteLine(message);
//        }
//
//        public void Write(string s)
//        {
//            writer.Write(s);
//        }
//    }
}
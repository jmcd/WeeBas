using System;

namespace WeeBas
{
    public static class EntryClass
    {
        public static void Main()
        {
            var vm = new Vm(Console.In, Console.Out, DidPressEscape);
            vm.Start();
        }

        private static bool DidPressEscape()
        {
            return Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape;
        }
    }
}
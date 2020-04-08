using System;

namespace TypingAppCLI.Controls
{
    class StatusStripCLI
    {
        private readonly static int _column = 0;
        private readonly static int _row = 21;

        internal static void Print(string message)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(_column, _row);
            Console.Write("{0}{1}", message, new string(' ',Console.WindowWidth - message.Length - 2));
            Console.ResetColor();
        }
    }
}

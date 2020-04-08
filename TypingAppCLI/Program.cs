using Core.Model;
using System;
using System.Linq;

namespace TypingAppCLI
{
    class Program
    {
        #region Fields

        private static ConsoleKeyInfo _keyPress;
        private static readonly int CONSOLE_WIDTH = 120;
        private static readonly int CONSOLE_HEIGHT = 22;

        private static readonly KeyPress[] _keyPresses = new KeyPress[]
        {
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Equals("o"), () => OpenListLessons()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Equals("q"), () => CloseApp()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Equals("l"), () => CancelListLessons()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Contains("d"), () => PickUpLesson()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Contains("r"), () => UseRevisionLesson()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Equals("s"), () => StartTypingLesson()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Equals("p"), () => PauseTypingLesson()),
            new KeyPress(k => k.Modifiers.HasFlag(ConsoleModifiers.Control) && k.Key.ToString().ToLower().Equals("x"), () => CancelTypingLesson()),
            new KeyPress(k => k.Key == ConsoleKey.Backspace, () => DeleteLetter()),
            new KeyPress(k => true, () => DoNothing() )
        };

        private static void UseRevisionLesson() => _appWindow.UseRevisionLesson();

        private static AppWindow _appWindow;

        #endregion

        #region commandMethods
        private static void OpenListLessons() => _appWindow.OpenListLessons();

        private static void CloseApp() => _appWindow.CloseApp();

        private static void CancelListLessons() => _appWindow.CancelListLessons();

        private static void PickUpLesson()
        {
            _appWindow.PickUpLesson(_keyPress.Key.ToString());
            _appWindow.txtLesson.Print();
        }

        private static void StartTypingLesson() => _appWindow.StartTypingLesson();

        private static void PauseTypingLesson() => _appWindow.PauseTypingLesson();

        private static void CancelTypingLesson() => _appWindow.CancelTypingLesson();

        private static void DeleteLetter() => _appWindow.txtTyping.DeleteLetter();

        private static void DoNothing() => _appWindow.DoNothing();

        #endregion

        static void Main(string[] args)
        {
            Initialize();
            _appWindow.Open();
            do
            {
                _keyPress = Console.ReadKey(true);
                var runShortCut = _keyPresses.First(x => x._canApply(_keyPress));
                runShortCut._menuAction();
            } while (_keyPress.Key != ConsoleKey.Escape);
        }

        #region Initialize

        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static void Initialize()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
            _appWindow = new AppWindow(new DataManager(), _keyPresses);
        }

        #endregion
    }
}

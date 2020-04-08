using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TypingAppCLI
{
    internal class TextBox
    {
        private static int _total;
        internal int Position => _total;
        private int _typingErrors;
        internal int TypingErrors { get => _typingErrors; set => _typingErrors = value; }
        private List<string> _mistypedWords;
        private KeyPress[] _keyPresses;
        private bool _enabled;
        internal bool Enabled { get => _enabled; set => _enabled = value; }
        private string _text;
        internal string Text { get => _text; set => _text = value; }
        internal Globals.LessonState _lessonState { get; set; }
        private static readonly int TYPING_ROW = 4;
        private static readonly int LESSON_TEXT_ROW = 3;

        public TextBox()
        {
            _lessonState = Globals.LessonState.LESSON_NOT_STARTED;
            _mistypedWords = new List<string>();
            _typingErrors = 0;
        }

        public TextBox(KeyPress[] keyPresses) : this()
        {
            _keyPresses = keyPresses;
        }

        public TextBox(string text, KeyPress[] keyPresses) : this(keyPresses)
        {
            _text = text;
            _keyPresses = keyPresses;
        }

        public List<string> GetMistypedWords() => _mistypedWords;

        readonly Action<string, int> markActualLetter = (s, i) =>
        {
            //Unmark previous typed letter
            if (i > 0)
            {
                Console.SetCursorPosition(i - 1, LESSON_TEXT_ROW);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(s[i - 1]);
                Console.SetCursorPosition(_total, TYPING_ROW);
            }
            //Mark a letter to be typed
            Console.SetCursorPosition(i, LESSON_TEXT_ROW);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(s[i]);
            Console.ResetColor();
            Console.SetCursorPosition(_total, TYPING_ROW);
            Console.CursorVisible = true;
        };

        void CheckForMistyping(string lessonText, int position, char typedLetter)
        {
            if (lessonText[position] != typedLetter)
            {
                _typingErrors++;
                var word = lessonText.GetWord(position);
                if (!string.IsNullOrEmpty(word))
                    _mistypedWords.Add(word);
            }
        }

        public bool StartTypingLesson(string lessonText)
        {
            _lessonState = Globals.LessonState.LESSON_START; //TODO-!!!!!
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
            while (_total < lessonText.Length && _lessonState.Equals(Globals.LessonState.LESSON_START))
            {
                //Marking the letter to be typed
                markActualLetter(lessonText, _total);

                keyPress = Console.ReadKey(true);
                var runShortCut = _keyPresses.First(x => x._canApply(keyPress));
                runShortCut._menuAction();

                //write to writing area
                //TODO-introduce TextBox here
                if (_lessonState.Equals(Globals.LessonState.LESSON_START) && _enabled && keyPress.Key != ConsoleKey.Backspace)
                {
                    markActualLetter(lessonText, _total);

                    CheckForMistyping(lessonText, _total, keyPress.KeyChar);

                    Console.Write(keyPress.KeyChar);
                    _total++;
                    Console.CursorVisible = true;
                }
                else Console.CursorVisible = false;
            }
            Console.CursorVisible = false;
            return _total == lessonText.Length;
        }

        internal void Print()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, LESSON_TEXT_ROW);
            Console.WriteLine("{0}", new string(' ', Console.LargestWindowWidth));
            Console.ResetColor();
            Console.SetCursorPosition(0, LESSON_TEXT_ROW);  //column,row
            Console.WriteLine(_text);
        }

        public void CancelTypingLesson()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, LESSON_TEXT_ROW);
            Console.WriteLine("{0}", new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, TYPING_ROW);
            Console.WriteLine("{0}", new string(' ', Console.WindowWidth));
            Console.ResetColor();
            _total = 0;
            Console.SetCursorPosition(_total, TYPING_ROW);
            _enabled = false;
        }

        public void DeleteLetter()
        {
            if (_total > 0) _total--;
            Console.SetCursorPosition(_total, TYPING_ROW);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" ");
            Console.SetCursorPosition(_total, TYPING_ROW);
            Console.ResetColor();
        }
    }
}
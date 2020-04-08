using System;
using System.Collections.Generic;
using TypingAppCLI.Controls.Menu;

namespace TypingAppCLI
{
    internal class VerticalMenu : MenuBase, IVerticalMenu
    {
        private readonly List<string> _lessons;

        public VerticalMenu(List<string> lessons):base()
        {
            _lessons = lessons;
            Initialize();
        }

        protected override void Initialize()
        {
            //TODO-get rid of these megic numbers
            int line = 20;
            int index = 1;
            foreach (var lesson in _lessons)
            {
                _menuItems.Add(new MenuItem(index.ToString(), lesson, 0, --line));
                index++;
            }
            _menuItems.Add(new MenuItem("L", "Cancel", 0, --line));
        }

        public void Erase()
        {
            foreach (var item in _menuItems)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(item._column, item._row);
                Console.WriteLine("{0}", new string(' ', Console.LargestWindowHeight));
                Console.ResetColor();
            }
        }
    }
}

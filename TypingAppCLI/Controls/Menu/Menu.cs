using System;
using System.Collections.Generic;

namespace TypingAppCLI.Controls.Menu
{
    public abstract class MenuBase
    {
        protected List<MenuItem> _menuItems;
        public MenuBase() => _menuItems = new List<MenuItem>();
        public virtual void Print()
        {
            foreach (var item in _menuItems)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(item._column, item._row);
                Console.Write("^{0}", item._shortCut);
                Console.ResetColor();
                Console.Write(" {0} ", item._menuText);
            }
        }
        protected abstract void Initialize();
    }
}

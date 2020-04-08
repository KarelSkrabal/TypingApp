using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingAppCLI
{
    internal class KeyPress
    {
        public Func<ConsoleKeyInfo, bool> _canApply { get; set; }
        public Action _menuAction { get; set; }

        public KeyPress(Func<ConsoleKeyInfo,bool> canApply, Action menuAction)
        {
            _canApply = canApply;
            _menuAction = menuAction;
        }
    }
}

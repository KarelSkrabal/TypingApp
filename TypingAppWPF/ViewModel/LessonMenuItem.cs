using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TypingAppWPF
{
    public class LessonMenuItem
    {
        private readonly ICommand _command;

        public LessonMenuItem()
        {
            _command = new MenuItemCommand(Execute);
        }

        public string Header { get; set; }
        public bool IsClickable { get; set; }

        public ObservableCollection<LessonMenuItem> MenuItems { get; set; }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }

        private void Execute()
        {
            // (NOTE: In a view model, you normally should not use MessageBox.Show()).
            if (IsClickable)
                MessageBox.Show("Clicked at " + Header);
        }
    }
}

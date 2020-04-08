using TypingAppCLI.Controls.Menu;

namespace TypingAppCLI
{
    internal class HorizontalMenu : MenuBase, IHorizontalMenu
    {
        public HorizontalMenu() : base() => Initialize();

        protected override void Initialize()
        {
            //TODO-magic numbers
            _menuItems.Add(new MenuItem("O", "Open Lesson", 0, 20));
            _menuItems.Add(new MenuItem("Q", "Close App", 19, 20));
            _menuItems.Add(new MenuItem("R", "Type revision lesson", 35, 20));
            _menuItems.Add(new MenuItem("S", "Start Typing", 62, 20));
            _menuItems.Add(new MenuItem("P", "Pause Typping", 80, 20));
            _menuItems.Add(new MenuItem("X", "Cancel Typping", 100, 20));
        }
    }
}

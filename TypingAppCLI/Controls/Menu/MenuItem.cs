namespace TypingAppCLI
{
    public class MenuItem
    {
        internal string _shortCut { get; set; }
        internal string _menuText { get;  set; }
        internal int _column { get; set; }
        internal int _row { get; set; }


        public MenuItem(string shortCut, string menuText, int column, int row)
        {
            _shortCut = shortCut;
            _menuText = menuText;
            _column = column;
            _row = row;
        }
    }
}

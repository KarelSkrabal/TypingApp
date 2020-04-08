using Core.Model;
using System;
using System.Windows.Forms;

namespace TypingAppWF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DataManager dataManager = new DataManager();
            MainForm view = new MainForm(dataManager);
            Application.Run(view);
        }
    }
}

using Core.Controller;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TypingAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IController _controller;
        public ObservableCollection<LessonMenuItem> MenuItems { get; set; }
        //private List<string> lessons = new List<string>() { "lesson 1", "lesson 2", "lesson 3" };
        public MainWindow()
        {
            InitializeComponent();
            _controller = new Controller(new DataManager());
            MenuItems = new ObservableCollection<LessonMenuItem>();
            foreach (var item in _controller.GetListLessons())
            {
                MenuItems.Add(new LessonMenuItem { Header = item, IsClickable = true });
            }
            MenuItems = new ObservableCollection<LessonMenuItem>
            {
                new LessonMenuItem {  Header = "Lessons", IsClickable = false , MenuItems = MenuItems }
            };
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //lessons = _controller.GetListLessons();

        }
    }
}

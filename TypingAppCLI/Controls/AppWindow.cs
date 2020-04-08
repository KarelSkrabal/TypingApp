using Core;
using Core.Interfaces;
using Core.Model;
using Core.Presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypingAppCLI.Controls;
using TypingAppCLI.Controls.Menu;

namespace TypingAppCLI
{
    internal class AppWindow : ILessonView
    {
        public TextBox txtTyping;
        public TextBox txtLesson;
        private List<string> _lessonsList;
        private IDataManager _dataManager;
        private readonly KeyPress[] _keyPresses;
        private static IHorizontalMenu _mainMenu;
        private static IVerticalMenu _listMenu;
        private static TimeSpan _totalTypingTimeSpan;
        private Presenter _presenter;
        private static Stopwatch _sw;
        private readonly int STATITISTICS_ROW = 12;
        private readonly int TYPING_TIME_ROW = 18;//column,row
        private readonly int TYPING_TIME_COLUMN = 0;
        private readonly int STATISTICS_ROW_SUM = 5;
        //field holding state of checkbox Type revision lesson [Ctrl+R]
        //each time when lesson is picked up reset to default value false
        //switch to value true when checkbox is checked, than initialize lesson text by revision text
        //if checkbox is switched to true & revision lesson exists
        private bool _useRevisionLesson = false;


        private async /*static*/ void RunMethodEvery(Action<string> method, double seconds)
        {
            while (_sw.IsRunning)
            {
                await Task.Delay(TimeSpan.FromSeconds(seconds));
                _totalTypingTimeSpan = TimeSpan.FromSeconds(Convert.ToInt32(_sw.Elapsed.TotalSeconds));
                method(_totalTypingTimeSpan.ToString("c"));
            }
            if (!_sw.IsRunning)
                EraseTypingTime();
        }

        public void StartTimer() => _sw.Start();

        public void StopTimer() => _sw.Stop();

        public void GetTypingTime(string typingTime)
        {
            if (txtTyping.Position <= Console.WindowWidth)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(TYPING_TIME_COLUMN, TYPING_TIME_ROW);
                Console.Write(typingTime);
                Console.SetCursorPosition(txtTyping.Position, 4);
            }
        }

        public void EraseTypingTime()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(TYPING_TIME_COLUMN, TYPING_TIME_ROW);
            Console.Write("{0}", new string(' ', Console.WindowWidth));
            Console.ResetColor();
        }

        public AppWindow(IDataManager dataManager, KeyPress[] keyPresses)
        {
            _dataManager = dataManager;
            _keyPresses = keyPresses;
            _presenter = new Presenter(this, dataManager);
            Initialize();
        }

        public void CloseApp()
        {
            StatusStripCLI.Print("Thank you for using TypingApp. Have a nice day");
            //TODO-sleep current thread for 1500 ms
            Environment.Exit(0);
        }

        public void OpenListLessons()
        {
            EraseStatistics();
            CancelTypingLesson();
            _listMenu.Print();
            StatusStripCLI.Print("[Ctrl+1..9] Pick up any lesson or[Ctrl + L] close the list of lessons");
        }

        public void CancelListLessons()
        {
            _listMenu.Erase();
            StatusStripCLI.Print("[Ctrl+O] Show a list of lessons or[Ctrl + Q] close application");
        }

        public void PickUpLesson(string lesson_PK)
        {
            _listMenu.Erase();
            txtLesson = new TextBox();
            
            _presenter.MenuItem_Click(lesson_PK);

            txtTyping = new TextBox(_keyPresses);
            StatusStripCLI.Print("[Ctrl+S] Start typing or[Ctrl+X] cancel lesson");
        }

        internal void CancelTypingLesson()
        {
            if (_presenter.LessonState.Equals(Globals.LessonState.LESSON_PICKED_UP)
                || _presenter.LessonState.Equals(Globals.LessonState.LESSON_PAUSE)
                || _presenter.LessonState.Equals(Globals.LessonState.LESSON_START))
            {
                _presenter.SetState(Globals.LessonState.LESSON_CANCEL);
                txtTyping.CancelTypingLesson();
                _presenter.ResetTimer();
                _presenter.EraseStatistics();
                _presenter.EraseTypingTime();
            }
        }

        internal void PauseTypingLesson()
        {
            //TODO-this is strange, do refactoring
            if (_presenter.LessonState.Equals(Globals.LessonState.LESSON_PICKED_UP) || _presenter.LessonState.Equals(Globals.LessonState.LESSON_START))
                _presenter.SetState(Globals.LessonState.LESSON_PAUSE);
            txtTyping.Enabled = false;
            Console.CursorVisible = false;
            txtTyping._lessonState = Globals.LessonState.LESSON_PAUSE;
            _presenter.StopTimer();
        }

        internal void StartTypingLesson()
        {
            if (_presenter.LessonState.Equals(Globals.LessonState.LESSON_PICKED_UP)
                || _presenter.LessonState.Equals(Globals.LessonState.LESSON_PAUSE)
                || _presenter.LessonState.Equals(Globals.LessonState.LESSON_START))
            {
                txtTyping.Enabled = true;
                _presenter.StartTimer();
                RunMethodEvery(GetTypingTime, 1);
                var lessonFinished = txtTyping.StartTypingLesson(txtLesson.Text);
                if (lessonFinished)
                {
                    _presenter.StopTimer();
                    _presenter.EraseTypingTime();
                    _presenter.GetStatisticsDetails(txtTyping.TypingErrors, txtTyping.GetMistypedWords(), _totalTypingTimeSpan);
                    _presenter.UpdateRevisionLessonText();
                }
            }
        }

        public void DoNothing() { }

        public void Open()
        {
            _presenter.SetState(Globals.LessonState.LESSON_NOT_STARTED);
            Console.CursorVisible = false;
            _mainMenu.Print();
            StatusStripCLI.Print("[Ctrl+O] Show a list of lessons or[Ctrl + Q] close application");
        }
        private void Initialize()
        {
            _mainMenu = new HorizontalMenu();
            _listMenu = new VerticalMenu(_lessonsList);
            //TODO-consider to move TextBox initialization to PickUpLesson
            txtTyping = new TextBox();
            _totalTypingTimeSpan = new TimeSpan();
            _lessonsList = new List<string>();
            _sw = new Stopwatch();
        }

        public void GetLessonList(List<string> lessonList) => _lessonsList = lessonList;

        public void SetLessonText(string lesson) => txtLesson.Text = lesson;

        public void SetControlsState(Globals.LessonState state)
        {
            switch (state)
            {
                case Globals.LessonState.LESSON_PICKED_UP:
                    _useRevisionLesson = false;
                    txtTyping._lessonState = txtLesson._lessonState = Globals.LessonState.LESSON_PICKED_UP;
                    break;
                case Globals.LessonState.LESSON_START:
                    _useRevisionLesson = false;
                    Console.CursorVisible = txtTyping.Enabled = true;
                    txtTyping._lessonState = txtLesson._lessonState = Globals.LessonState.LESSON_START;
                    break;
                case Globals.LessonState.LESSON_PAUSE:
                    _useRevisionLesson = false;
                    Console.CursorVisible = txtTyping.Enabled = false;
                    txtTyping._lessonState = txtLesson._lessonState = Globals.LessonState.LESSON_PAUSE;
                    break;
                case Globals.LessonState.LESSON_CANCEL:
                    _useRevisionLesson = false;
                    txtTyping.Enabled = false;
                    txtTyping._lessonState = Globals.LessonState.LESSON_CANCEL;
                    if (txtLesson != null)
                        txtLesson._lessonState = Globals.LessonState.LESSON_CANCEL;
                    break;
                case Globals.LessonState.LESSON_NOT_STARTED:
                    txtTyping.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        public void OutPutStatistics(List<string> statistics)
        {
            int row = STATITISTICS_ROW;
            foreach (var item in statistics)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(item);
                row++;
            }
        }
        public void EraseStatistics()
        {
            for (int i = 0; i < STATISTICS_ROW_SUM; i++)
            {
                Console.SetCursorPosition(0, STATITISTICS_ROW + i);
                Console.Write("{0}", new string(' ', Console.WindowWidth));
            }
        }
        //TODO-violation of Liskov principle
        public void SetTypingTime(string writingTime)
        {
            throw new NotImplementedException();
        }

        public void PerformCancelTyping()
        {
            throw new NotImplementedException();
        }

        public void UpdateRevisionLessonText()
        {
            throw new NotImplementedException();
        }
        public void MarkLessonText(RichTextBox control)
        {
            throw new NotImplementedException();
        }

        public void SetUseRevisionLesson(bool revisionLessonAvailable)
        {
            _useRevisionLesson = !_useRevisionLesson;
        }

        internal void UseRevisionLesson()
        {
            if(txtLesson._lessonState == Globals.LessonState.LESSON_PICKED_UP)
            {
                _presenter.SetUseRevisionLesson();
                _presenter.SetLessonText(_useRevisionLesson);
                txtLesson.Print();
            }
        }

        public void ResetTimer()
        {
            _sw.Reset();
        }
    }
}

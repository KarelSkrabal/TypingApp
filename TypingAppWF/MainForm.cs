using Core;
using Core.Interfaces;
using Core.Model;
using Core.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TypingAppWF
{
    public partial class MainForm : Form, ILessonView
    {
        private Presenter _presenter;
        private int _beepSpeed;
        private static TimeSpan _totalTypingTimeSpan;
        private Stopwatch _sw;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(DataManager dataManager) : this()
        {
            _presenter = new Presenter(this, dataManager);
            _presenter.SetState(Globals.LessonState.LESSON_NOT_STARTED);
            this.ActiveControl = null;
            Initialize();
        }

        private void Initialize()
        {
            bgwBeeping.WorkerSupportsCancellation = true;
            nudSpeedBeeping.Minimum = 300;
            nudSpeedBeeping.Maximum = 2000;
            nudSpeedBeeping.Increment = 200;
            //TODO-value shown in control represents a length of delay,it doesn't correspond with the user's understanding of that value
            //actually the user understands that as an amount of key strokes per minute, value shown should be recalculated, value passed
            //as parameter to BgwBeeping_DoWork event via _beepSpeed field has to be keeped as it is.
            nudSpeedBeeping.Value = 800;
            _totalTypingTimeSpan = new TimeSpan();
            _sw = new Stopwatch();
        }

        private void LessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lesson_PK = ((ToolStripMenuItem)sender).Tag.ToString();
            _presenter.MenuItem_Click(((ToolStripMenuItem)sender).Tag.ToString());
            _presenter.SetState(Globals.LessonState.LESSON_PICKED_UP);
            _presenter.SetLessonText();
            _presenter.EraseStatistics();
            _presenter.SetUseRevisionLesson();
            _presenter.EraseTypingTime();
        }

        public void SetLessonText(string lesson) => rtxtLesson.Text = lesson;

        public void SetControlsState(Globals.LessonState state)
        {
            switch (state)
            {
                case Globals.LessonState.LESSON_PICKED_UP:
                    btnStartBeeping.Enabled = btnStopBeeping.Enabled = nudSpeedBeeping.Enabled = btnCancelTyping.Enabled = btnStartTyping.Enabled = true;
                    rtxtTyping.Text = string.Empty;
                    rtxtTyping.Enabled = false;
                    btnStartTyping.Focus();
                    break;
                case Globals.LessonState.LESSON_START:
                    rtxtTyping.Enabled = true;
                    rtxtTyping.Focus();
                    btnCancelTyping.Enabled = btnPauseTyping.Enabled = true;
                    chUseRevisionLesson.Enabled = btnStartTyping.Enabled = false;
                    break;
                case Globals.LessonState.LESSON_PAUSE:
                    chUseRevisionLesson.Enabled = btnPauseTyping.Enabled = false;
                    btnStartTyping.Enabled = true;
                    btnStartTyping.Focus();
                    break;
                case Globals.LessonState.LESSON_CANCEL:
                    chUseRevisionLesson.Enabled = btnCancelTyping.Enabled = btnPauseTyping.Enabled = btnStartTyping.Enabled = rtxtTyping.Enabled = false;
                    btnStartBeeping.Enabled = btnStopBeeping.Enabled = nudSpeedBeeping.Enabled = false;
                    rtxtLesson.Text = rtxtTyping.Text = string.Empty;
                    break;
                case Globals.LessonState.LESSON_NOT_STARTED:
                    btnStartBeeping.Enabled = btnStopBeeping.Enabled = nudSpeedBeeping.Enabled = chUseRevisionLesson.Enabled = btnCancelTyping.Enabled = false;
                    btnPauseTyping.Enabled = btnStartTyping.Enabled = rtxtTyping.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void BtnStartTyping_Click(object sender, EventArgs e)
        {
            _presenter.SetState(Globals.LessonState.LESSON_START);
            _presenter.MarkLessonText(rtxtLesson, null);
            _presenter.StartTimer();
        }

        private void RtxtTyping_KeyPress(object sender, KeyPressEventArgs e)
        {
            _presenter.CheckForEndOfLesson(rtxtLesson.Text, rtxtTyping.TextLength);
            _presenter.MarkLessonText(rtxtLesson, e.KeyChar);
            _presenter.CheckForMistyping(rtxtTyping.TextLength, e.KeyChar);
        }

        public void MarkLessonText(RichTextBox control) => rtxtLesson = control;

        private void BtnPauseTyping_Click(object sender, EventArgs e)
        {
            _presenter.SetState(Globals.LessonState.LESSON_PAUSE);
            _presenter.StopTimer();
        }

        private void BtnCancelTyping_Click(object sender, EventArgs e) => PerformCancelTyping();

        public void PerformCancelTyping()
        {
            _presenter.SetState(Globals.LessonState.LESSON_CANCEL);
            bgwBeeping.CancelAsync();
            _presenter.ResetTimer();
            _presenter.EraseTypingTime();
        }

        public void EraseTypingTime() => lbWritingTimeSpan.Text = string.Empty;

        public void GetLessonList(List<string> lessonList)
        {
            foreach (var item in lessonList)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(item)
                {
                    Tag = item,
                    //ShortcutKeys = Keys.Control | (Keys)new KeysConverter().ConvertFromString(Regex.Match(item, @"\d+").Value),
                    //ShowShortcutKeys = true
                };
                subItem.Click += LessonToolStripMenuItem_Click;
                openLessonToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }

        private void nudSpeed_ValueChanged(object sender, EventArgs e) => _beepSpeed = Convert.ToInt32((sender as NumericUpDown).Value);

        private void nudSpeed_Leave(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            if (bgwBeeping.IsBusy != true)
                bgwBeeping.RunWorkerAsync();
        }

        private void BgwBeeping_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (; ; )
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(_beepSpeed);
                    Console.Beep(900, 300);
                }
            }
        }

        private void BtnStartBeeping_Click(object sender, EventArgs e)
        {
            if (bgwBeeping.IsBusy != true)
                bgwBeeping.RunWorkerAsync();
            rtxtTyping.Focus();
        }


        private void btnStopBeeping_Click(object sender, EventArgs e)
        {
            bgwBeeping.CancelAsync();
            _presenter.StopTimer();
        }

        private void NudSpeed_MouseDown(object sender, MouseEventArgs e) => bgwBeeping.CancelAsync();

        public void StartTimer() => _sw.Start();

        public void StopTimer() => _sw.Stop();

        public void ResetTimer() => _sw.Reset();

        public void GetTypingTime(string typingTime)
        {
            throw new NotImplementedException();
        }

        public void OutPutStatistics(List<string> statistics)
        {
            StringBuilder statisticsText = new StringBuilder();
            foreach (var item in statistics)
                statisticsText.AppendLine(item);
            lbStatistics.Text = statisticsText.ToString();
            _presenter.SetState(Globals.LessonState.LESSON_NOT_STARTED);
        }

        public void EraseStatistics() => lbStatistics.Text = string.Empty;



        private void TmTimer_Tick(object sender, EventArgs e)
        {
            _totalTypingTimeSpan = TimeSpan.FromSeconds(Convert.ToInt32(_sw.Elapsed.TotalSeconds));
            _presenter.SetTypingTime(_totalTypingTimeSpan);
        }

        public void SetTypingTime(string writingTime) => lbWritingTimeSpan.Text = writingTime;

        public void UpdateRevisionLessonText()
        {
            throw new NotImplementedException();
        }

        public void SetUseRevisionLesson(bool revisionLessonAvailable)
        {
            chUseRevisionLesson.Enabled = revisionLessonAvailable;
        }

        private void chUseRevisionLesson_CheckedChanged(object sender, EventArgs e)
        {
            _presenter.SetLessonText(chUseRevisionLesson.Checked);
        }
    }
}

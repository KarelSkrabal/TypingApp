using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Core.Interfaces
{
    public interface ILessonView
    {
        void GetLessonList(List<string> lessonList);
        void SetLessonText(string lesson);
        void SetControlsState(Globals.LessonState state);
        void MarkLessonText(RichTextBox control);
        void StartTimer();
        void StopTimer();
        void ResetTimer();
        void GetTypingTime(string typingTime);
        void OutPutStatistics(List<string> statistics);
        void EraseStatistics();
        void EraseTypingTime();
        void SetTypingTime(string writingTime);
        void PerformCancelTyping();
        void UpdateRevisionLessonText();
        void SetUseRevisionLesson(bool revisionLessonAvailable);
    }
}

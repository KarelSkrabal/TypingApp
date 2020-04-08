using Core.Model;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ILessonViewModel
    {
        ILessonModel LessonModelEnity { get; }
        int Lesson_PK { get; }
        string LessonText { get; set; }
        string RevisionLessonText { get; set; }
        Globals.LessonState LessonState { get; set; }
        TimeSpan TypingTime { get; set; }
        int TypingErrors { get; set; }
        List<string> MistypedWords { get; set; }
        bool LessonFinishedUp { get; set; }
        bool IsRevisionAvailable { get; set; }
    }
}

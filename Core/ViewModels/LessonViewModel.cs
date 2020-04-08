using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Model;

namespace Core.ViewModels
{
    /// <summary>
    /// ViewModel for Lesson Entity
    /// </summary>
    public class LessonViewModel : ILessonViewModel
    {
        //TODO-rename CorrectedLessonText to something like MistypedWords, 
        private readonly ILessonModel _lessonModel;
        public ILessonModel LessonModelEnity { get => _lessonModel; }
        public string LessonText { get => _lessonModel.LessonText; set => _lessonModel.LessonText = value; }
        public string RevisionLessonText { get => _lessonModel.RevisionLessonText; set => _lessonModel.RevisionLessonText = value; }
        public Globals.LessonState LessonState { get; set; }
        public TimeSpan TypingTime { get; set; }
        public int TypingErrors { get; set; }
        public List<string> MistypedWords { get; set; }
        public int Lesson_PK { get => _lessonModel.Lesson_PK; }
        public bool LessonFinishedUp { get; set; }
        public bool IsRevisionAvailable
        {
            get => (string.IsNullOrEmpty(_lessonModel.RevisionLessonText)) ? false : true;
            set => value = (string.IsNullOrEmpty(_lessonModel.RevisionLessonText)) ? false : true;
        }

        public LessonViewModel(ILessonModel lessonModel)
        {
            _lessonModel = lessonModel;
            TypingErrors = 0;
            MistypedWords = new List<string>();
            LessonFinishedUp = false;
        }
    }
}

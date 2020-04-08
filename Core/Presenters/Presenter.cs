using Core.Interfaces;
using Core.Model;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TypingAppWF;

namespace Core.Presenters
{
    public class Presenter
    {
        private readonly ILessonView _mainFormView;
        private readonly IDataManager _dataManager;
        private ILessonViewModel _lessonViewModel;
        private static int _letterTyped;

        public Globals.LessonState LessonState { get => _lessonViewModel.LessonState; }

        public Presenter(ILessonView mainFormView, IDataManager dataManager)
        {
            _mainFormView = mainFormView;
            _dataManager = dataManager;
            Initialize();
        }

        private void Initialize()
        {
            _lessonViewModel = new LessonViewModel(new LessonModelEntity());
            PopulateMenuList();
        }

        void IsMistyping(int position, char typedLetter)
        {
            if (_lessonViewModel.LessonText[position] != typedLetter)
                _lessonViewModel.TypingErrors++;
        }

        public void StartTimer() => _mainFormView.StartTimer();
        public void StopTimer() => _mainFormView.StopTimer();

        private void PopulateMenuList() => _mainFormView.GetLessonList(_dataManager.GetListLessons());

        public void SetState(Globals.LessonState state)
        {
            _lessonViewModel.LessonState = state;
            _mainFormView.SetControlsState(_lessonViewModel.LessonState);
        }

        public void MarkLessonText(RichTextBox control, char? key)
        {
            control.Tag = key;
            new MarkTextController().MarkText(control, ref _letterTyped);
            _mainFormView.MarkLessonText(control);
        }

        public void EraseTypingTime() => _mainFormView.EraseTypingTime();

        private void SetControlsState() => _mainFormView.SetControlsState(_lessonViewModel.LessonState);

        public void MenuItem_Click(string lesson_PK)
        {
            var lesson = _dataManager.GetLessonById(Convert.ToInt32(Regex.Match(lesson_PK, @"\d+").Value));
            _lessonViewModel = new LessonViewModel(lesson);
            SetState(Globals.LessonState.LESSON_PICKED_UP);
            SetLessonText();
            _letterTyped = 0;
        }
        public void SetLessonText(bool useRevisionLesson = false)
        {
            if (useRevisionLesson && _lessonViewModel.IsRevisionAvailable)
                _mainFormView.SetLessonText(_lessonViewModel.RevisionLessonText);
            else
                _mainFormView.SetLessonText(_lessonViewModel.LessonText);
        }


        public void GetStatisticsDetails(int typingErrors, List<string> mistypedWords, TimeSpan totalTypingTimeSpan)
        {
            _lessonViewModel.TypingErrors = typingErrors;
            _lessonViewModel.MistypedWords = mistypedWords;
            _lessonViewModel.TypingTime = totalTypingTimeSpan;
            _mainFormView.OutPutStatistics(CreateStatistics());
        }

        private List<string> CreateStatistics()
        {
            //TODO-typed letters per minute it's wrongly calculated
            List<string> statistics = new List<string>();
            statistics.Add("Time writting ............. : " + _lessonViewModel.TypingTime.ToString(@"mm\:ss"));
            statistics.Add("Length of the lesson ...... : " + _lessonViewModel.LessonText.Length.ToString());
            statistics.Add("Typed letters per minute .. : " + (Math.Ceiling(_lessonViewModel.TypingTime.TotalMinutes * 60 * 60 / _lessonViewModel.LessonText.Length)).ToString());
            statistics.Add("Sum of errors ............. : " + _lessonViewModel.TypingErrors.ToString());
            statistics.Add("Error rate ................ : " + (_lessonViewModel.TypingErrors * 100 / _lessonViewModel.LessonText.Length).ToString() + " %");
            return statistics;
        }

        public void EraseStatistics() => _mainFormView.EraseStatistics();

        //TODO-this same method is declared in TextBox class, consider reusing it somehow in TextBox via presenter
        //consider usage _lessonViewModel field that won't be available if passing this method as delegate
        //this time the methods is called just in WF project
        public void CheckForMistyping(int position, char typedLetter)
        {
            if (_lessonViewModel.LessonText.Length >= (position + 1) && _lessonViewModel.LessonText[position] != typedLetter)
            {
                _lessonViewModel.TypingErrors++;
                var word = _lessonViewModel.LessonText.GetWord(position);
                if (!string.IsNullOrEmpty(word))
                    _lessonViewModel.MistypedWords.Add(word);
            }
        }

        public void CheckForEndOfLesson(string lessonText, int position)
        {
            if (lessonText.Length.Equals(position + 1))
            {
                _lessonViewModel.LessonFinishedUp = true;
                _mainFormView.PerformCancelTyping();
                _mainFormView.OutPutStatistics(CreateStatistics());
                if (_lessonViewModel.MistypedWords.Count > 0)
                    UpdateRevisionLessonText();
            }
        }

        public void ResetTimer() => _mainFormView.ResetTimer();

        public void SetTypingTime(TimeSpan writingTime)
        {
            _lessonViewModel.TypingTime = writingTime;
            _mainFormView.SetTypingTime(" " + _lessonViewModel.TypingTime.ToString(@"mm\:ss"));
        }

        public void UpdateRevisionLessonText()
        {
            if (_lessonViewModel.MistypedWords.Count > 0)
                _dataManager.UpdateRevisionText(_lessonViewModel.Lesson_PK, CreateRevisionLessonText());
        }

        private string CreateRevisionLessonText()
        {
            //split to words
            List<string> wordsToAdd = new List<string>(_lessonViewModel.LessonText.Split(' '));
            //remove doubled words
            List<string> wordsToAddDistinct = wordsToAdd.Distinct().ToList<string>();
            List<string> misttypedWordsDistinct = _lessonViewModel.MistypedWords.Distinct().ToList<string>();
            //remove words typed without errors 
            wordsToAddDistinct.RemoveAll(misttypedWordsDistinct.Contains);
            //create list of mistypedwords each word is doubled
            List<string> doubledMistypedWords = misttypedWordsDistinct;
            doubledMistypedWords.AddRange(misttypedWordsDistinct);
            //create a list containing a mistyped word following successfully written word
            //there will be an each occurance of mistyped word created twice
            List<string> revisionTextList = new List<string>();
            int index = 0;
            foreach (var word in doubledMistypedWords)
            {
                revisionTextList.Add(word);
                if (wordsToAddDistinct.Count > 0)
                {
                    if (index >= wordsToAddDistinct.Count)
                        index = index - wordsToAddDistinct.Count;
                    revisionTextList.Add(wordsToAddDistinct[index]);
                    index++;
                }
            }
            //create a revision text by copiing created list of words to the max length of lesson
            int maxLength = 119;
            string revisionText = string.Join(" ", revisionTextList);
            int howManyTimesCopy = (maxLength / revisionText.Length) + 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < howManyTimesCopy; i++)
                sb.Append(revisionText);
            revisionText = sb.ToString().Substring(0, maxLength);

            return revisionText;
        }

        public void SetUseRevisionLesson() => _mainFormView.SetUseRevisionLesson(_lessonViewModel.IsRevisionAvailable);
    }
}

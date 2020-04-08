using Core.Interfaces;
using Core.Model;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Core.Presenters
{
    public class Presenter
    {
        private readonly ILessonView _mainFormView;
        private readonly IDataManager _dataManager;
        private readonly ILessonViewModel _lessonViewModel;

        public Presenter(ILessonView mainFormView, IDataManager dataManager)
        {
            _mainFormView = mainFormView;
            _dataManager = dataManager;
            _lessonViewModel = new LessonViewModel(new LessonModelEntity());
            Update();
        }

        //TODO-
        private void Update()
        {
            //Get lesson list and populate menu
            _mainFormView.GetLessonList(_dataManager.GetListLessons());
        }

        public void SetState(Globals.LessonState state)
        {
            _lessonViewModel.LessonState = state;

            MainForm view = new MainForm();
                view.btnCancelTyping.Enabled = false;
        }

        public Keys GetKeyByNum(string number) => Keys.Control | (Keys)new KeysConverter().ConvertFromString(Regex.Match(number, @"\d+").Value);
    }
}

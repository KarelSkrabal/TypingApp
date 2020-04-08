using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Core.Model
{
    public class DataManager : IDataManager
    {
        private readonly string DATA_FILE_NAME = "Lessons.xml";
        private readonly string _dataFilePath;
        private XDocument _xmldoc;
        private List<LessonModelEntity> _lessons = new List<LessonModelEntity>();

        public DataManager()
        {
            try
            {
                _dataFilePath = Path.Combine(Environment.CurrentDirectory, DATA_FILE_NAME);
                if (File.Exists(_dataFilePath))
                    Initialize();
                else throw new FileNotFoundException();
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }

        /// <summary>
        /// Loading all lessons data
        /// </summary>
        private void Initialize()
        {
            _xmldoc = XDocument.Load(_dataFilePath);
            var xmlData = _xmldoc.Descendants("Lesson").Select(x => new
            {
                Lesson_PK = x.Element("Lesson_PK").Value,
                LessonName = x.Element("LessonName").Value,
                LessonText = x.Element("LessonText").Value,
                RevisionLessonText = x.Element("RevisionLessonText").Value,
            }).OrderBy(x => x.Lesson_PK).ToList();

            //It has to be cleared before adding items,cause it could be called repeatedly when updating revision lesson text
            _lessons.Clear();

            foreach (var item in xmlData)
            {
#pragma warning disable IDE0017 // Simplify object initialization
                LessonModelEntity lesson = new LessonModelEntity();
#pragma warning restore IDE0017 // Simplify object initialization
                lesson.Lesson_PK = Convert.ToInt32(item.Lesson_PK);
                lesson.LessonName = item.LessonName;
                lesson.LessonText = item.LessonText;
                lesson.RevisionLessonText = item.RevisionLessonText;
                _lessons.Add(lesson);
            }

        }

        /// <summary>
        /// Gets lesson
        /// </summary>
        /// <param name="id">PK of the lesson</param>
        /// <returns></returns>
        public LessonModelEntity GetLessonById(int id)
        {
            Initialize();
            return _lessons.FirstOrDefault(x => x.Lesson_PK == id);
        }

        public void UpdateRevisionText(int id, string revisionText)
        {
            XElement emp = _xmldoc.Descendants("Lesson").FirstOrDefault(x => x.Element("Lesson_PK").Value == id.ToString());
            if (emp != null)
            {
                emp.SetElementValue("RevisionLessonText", revisionText);
                _xmldoc.Save(_dataFilePath);
            }
        }

        /// <summary>
        /// Returns list of lessons read from data entity. The list returned is used to populate menu to pick up a lesson.
        /// </summary>
        /// <returns>List of lessons</returns>
        public List<string> GetListLessons()
        {
            List<string> lessonNames = new List<string>();
            foreach (var name in _lessons)
            {
                lessonNames.Add(name.LessonName);
            }
            return lessonNames;
        }

        /// <summary>
        /// Counts lessons
        /// </summary>
        /// <returns>Total number of lessons</returns>
        public int Count() => _lessons.Count;

        public LessonModelEntity GetLessonsByName(string lessonName) => _lessons.FirstOrDefault(x => x.LessonName.Equals(lessonName));

        /// <summary>
        /// Checkes if lesson exists in entity data
        /// </summary>
        /// <param name="id">Lesson id (primery key)</param>
        /// <returns></returns>
        public bool IsValidLesson(int id) => _lessons.Exists(x => x.Lesson_PK == id);
    }
}

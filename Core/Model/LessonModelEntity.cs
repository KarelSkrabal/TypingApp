namespace Core.Model
{
    public class LessonModelEntity : ILessonModel
    {
        public int Lesson_PK { get; set; }
        public string LessonName { get; set; }
        public string LessonText { get; set; }
        public string RevisionLessonText { get; set; }
    }
}

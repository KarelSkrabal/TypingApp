namespace Core.Model
{
    public interface ILessonModel
    {
        int Lesson_PK { get; set; }
        string LessonName { get; set; }
        string LessonText { get; set; }
        string RevisionLessonText { get; set; }
    }
}

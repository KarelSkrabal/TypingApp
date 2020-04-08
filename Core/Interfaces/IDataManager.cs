using System.Collections.Generic;

namespace Core.Model
{
    public interface IDataManager
    {
        void UpdateRevisionText(int id, string revisionText);
        LessonModelEntity GetLessonById(int id);
        List<string> GetListLessons();
    }
}

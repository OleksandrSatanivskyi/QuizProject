using System;

namespace QuizProject.Running
{
    public interface IObjectManager<T>
    {
        void CreateObject();
        T GetObject();
        void RenameObject();
        void DeleteObject();
        
    }
}

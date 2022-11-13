using System;

namespace QuizProject.Running
{
    public interface IObjectCommandCollection<T>
    {
        void CreateObject();
        T GetObject();
        void RenameObject();
        void DeleteObject();
        
    }
}

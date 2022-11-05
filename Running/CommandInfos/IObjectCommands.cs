using System;

namespace QuizProject.Running
{
    public interface IObjectCommands<T>
    {
        void CreateObject();
        T GetObject();
        void RenameObject();
        void DeleteObject();
        
    }
}

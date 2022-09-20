using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

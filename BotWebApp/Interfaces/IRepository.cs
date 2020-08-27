using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Interfaces
{
    interface IRepository<T>: IDisposable where T: class
    {
        void Create(T item);
        void Save(T item);
        void Delete(T item);
        void Read(T item);

    }
}

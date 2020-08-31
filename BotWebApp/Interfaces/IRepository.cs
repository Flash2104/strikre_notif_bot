using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Interfaces
{
    interface IRepository<T> where T: IEntity
    {        
        Task<T> CreateAsync(T item);
        T Update(T item);        
        void Delete(int id);
        Task<T> ReadAsync(int id);
        Dictionary<int, T> GetAll();
    }
}

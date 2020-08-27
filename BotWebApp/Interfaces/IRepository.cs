using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Interfaces
{
    interface IRepository<T> where T: IEvent
    {        
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);        
        void Delete(int id);
        Task<T> ReadAsync(int id);

    }
}

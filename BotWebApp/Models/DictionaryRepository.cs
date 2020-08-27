using BotWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Models
{
    public class DictionaryRepository<T> : IRepository<T> where T: IEvent
    {
        private Dictionary<int, T> dict = new Dictionary<int, T>();
        public Task<T> CreateAsync(T item)
        {
            dict.Add(item.Id, item);
            return new Task<T>(() => item);
        }

        public void Delete(int id)
        {
            dict.Remove(id);            
        }       

        public Task<T> ReadAsync(int id)
        {
            return new Task<T>(() => dict[id]);
        }

        public Task<T> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}

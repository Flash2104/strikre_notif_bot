using BotWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Models
{
    public class DictionaryRepository<T> : IRepository<T> where T: IEntity
    {
        private Dictionary<int, T> dict = new Dictionary<int, T>();        
        public Task<T> CreateAsync(T item)
        {
            int generatedId = dict.Keys.Last();
            while (dict.ContainsKey(++generatedId))
            {
                generatedId++;
            }
            item.Id = generatedId;
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

        public T Update(T item)
        {
            return item;
        }
        public Dictionary<int, T> GetAll()
        {
            return dict;
        }
    }
}

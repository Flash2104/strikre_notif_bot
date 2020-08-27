using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Interfaces
{
    public interface IEvent
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Date { get; set; }
        string Comment { get; set; }
        string User { get; set; }
    }
}

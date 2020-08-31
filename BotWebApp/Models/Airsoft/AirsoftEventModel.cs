using BotWebApp.Interfaces;
using BotWebApp.State.Airsoft;
using System;

namespace BotWebApp.Models.Airsoft
{
    public class AirsoftEventModel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public AirsoftEventStates State { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Models
{
    public class AirsoftEventContext: DbContext
    {
        public DbSet<AirsoftEvent> AirsoftEvents { get; set; }
    }
}

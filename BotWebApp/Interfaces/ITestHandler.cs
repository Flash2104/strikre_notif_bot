using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.Interfaces
{
    interface ITestHandler
    {
        void Handle(string command, long botChatId);
    }
}

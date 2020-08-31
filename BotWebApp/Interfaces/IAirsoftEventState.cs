using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotWebApp.Interfaces
{
    public interface IAirsoftEventState
    {
        void HandleMessage(Message message);
        bool TryHandleMessage(Message message);
        void SendInvintation();
        void Next();
        void Previous();

    }
}

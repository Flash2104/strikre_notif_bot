using BotWebApp.Interfaces;
using Telegram.Bot.Types;

namespace BotWebApp.Models.Airsoft
{
    public abstract class AirsofEventStateBase : IAirsoftEventState
    {
        public void HandleMessage(Message message)
        {
           if (TryHandleMessage(message))
            {
                this.Next();
            }
        }

        public abstract void Next();

        public abstract void Previous();

        public abstract void SendInvintation();

        public abstract bool TryHandleMessage(Message message);
    }
}

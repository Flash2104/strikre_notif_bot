using System;
using BotWebApp.Interfaces;
using BotWebApp.Models;

namespace BotWebApp.Services
{
    public class TestButtonHandler: ITestHandler
    {
        public void Handle(string button, long botChatId)
        {
            if (Enum.TryParse<TestButtons>(button, out var but))
            {
                switch (but)
                {
                    case TestButtons.TestButton1:
                    {
                        BotClient.Instance.Bot.SendTextMessageAsync(botChatId, "Кнопка 1 принята!");
                        break;
                    }
                    case TestButtons.TestButton2:
                    {
                        BotClient.Instance.Bot.SendTextMessageAsync(botChatId, "Кнопка 2 принята!");
                        break;
                    }
                    default:
                    {
                        BotClient.Instance.Bot.SendTextMessageAsync(botChatId, "Команда не распознана!");
                        break;
                    }
                }
            }
        }
    }
}
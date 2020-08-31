using System.Collections.Generic;
using BotWebApp.Interfaces;
using BotWebApp.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotWebApp.Services
{
    public class TestCommandHandler: ITestHandler
    {
        public TestCommandHandler()
        {
        }

        public void Handle(string command, long botChatId)
        {
            if (CommandsMap.TestCommandsMap.TryGetValue(command.TrimStart('/'), out var com))
            {
                switch (com)
                {
                    case TestCommands.TestCommand1:
                        {
                            BotClient.Instance.Bot.SendTextMessageAsync(botChatId, "Тестовый опросник",
                                replyMarkup: new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
                                {
                                new InlineKeyboardButton(){Text = "Кнопка 1", CallbackData = TestButtons.TestButton1.ToString() },
                                new InlineKeyboardButton(){Text = "Кнопка 2", CallbackData = TestButtons.TestButton2.ToString() }
                            }));
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
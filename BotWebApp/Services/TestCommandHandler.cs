using System.Collections.Generic;
using BotWebApp.Interfaces;
using BotWebApp.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotWebApp.Services
{
    public class TestCommandHandler: ITestCommandHandler
    {
        public TestCommandHandler()
        {
        }

        public void HandleCommand(string command, long botChatId)
        {
            if (Commands.TestCommandsMap.TryGetValue(command.TrimStart('/'), out var com))
            {
                switch (com)
                {
                    case TestCommands.TestCommand1:
                        {
                            BotClient.Instance.Bot.SendTextMessageAsync(botChatId, "Тестовый опросник",
                                replyMarkup: new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
                                {
                                new InlineKeyboardButton(){Text = "Кнопка 1", CallbackData = "Кнопка 1"},
                                new InlineKeyboardButton(){Text = "Кнопка 2", CallbackData = "Кнопка 2"}
                            }));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
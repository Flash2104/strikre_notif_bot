using System;
using BotWebApp.Interfaces;
using BotWebApp.Services;
using NLog;
using NLog.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace BotWebApp
{
    public class BotClient: IBotClient
    {
        private static readonly object instanceLock = new object();

        private static volatile BotClient instance;

        public static BotClient Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BotClient();
                    }
                }
                return instance;
            }
        }

        private readonly ILogger _logger;
        public ITelegramBotClient Bot { get; }

        private readonly string _logChannelId;
        private readonly ITestHandler _testCommandHandler;
        private readonly ITestHandler _testButtonHandler;

        public BotClient()
        {
            _logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            _logger.Info("BotClient is initialized");
            var configuration = ConfigurationModel.GetConfiguration();
            _logChannelId = configuration.TelegramConfiguration.LogChannelId;
            Bot = new TelegramBotClient(configuration.TelegramConfiguration.BotToken);
            var r = Bot.TestApiAsync().GetAwaiter().GetResult();
            _logger.Info($"Test bot request: {r}");
            _testCommandHandler = new TestCommandHandler();
            _testButtonHandler = new TestButtonHandler();
            Bot.OnMessage += ProcessMessage;
            Bot.OnCallbackQuery += BotOnOnCallbackQuery;
            Bot.StartReceiving();
        }

        public void Init() { }

        private void BotOnOnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var query = callbackQueryEventArgs.CallbackQuery;
            string user = GetUserOrGroupName(query.Message);
            try
            {
                _logger.Info("Bot OnOnCallbackQuery");
                query.Message.Text = query.Data;
                _logger.Info($"OnOnCallbackQuery message: \"{ query.Message?.Text}\"; chatId: { query.Message?.Chat.Id}");
                var resp = Bot.SendTextMessageAsync(new ChatId(_logChannelId),
                    $"OnOnCallbackQuery Command: \"{query.Message.Text}\". User: {user}.\n").GetAwaiter().GetResult();
                _logger.Info($"Logs channel message: \"{resp?.Text}\"; chatId: \"{resp?.Chat?.Id}\"");
                HandleCallBack(query.Message);
            }
            catch (Exception e)
            {
                var messageEx = $"OnOnCallbackQuery Exception:\n" +
                                $"Command: \"{query.Message.Text}\".\n" +
                                $"User: {user}.\n" +
                                $"Message: {e.Message}\n" +
                                $"StackTrace: {e.StackTrace}.\n";
                _logger.Error(messageEx);
                Bot.SendTextMessageAsync(_logChannelId, messageEx);
            }
        }

        private void ProcessMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            string user = GetUserOrGroupName(message);
            try
            {
                _logger.Info("Bot ProcessMessage");
                _logger.Info($"ProcessMessage message: \"{message.Text}\"; chatId: \"{message.Chat.Id}\"");
                var resp = Bot.SendTextMessageAsync(new ChatId(_logChannelId),
                    $"ProcessMessage Command: \"{message.Text}\". User: {user}.\n").GetAwaiter().GetResult();
                _logger.Info($"Logs channel message: \"{resp?.Text}\"; chatId: \"{resp?.Chat?.Id}\"");
                HandleMessage(message);
                var resp1 = Bot.SendTextMessageAsync(message.Chat.Id, BotResponse(message));
            }
            catch (Exception e)
            {
                var messageEx = $"ProcessMessage Exception:\n" +
                                $"Command: \"{message.Text}\".\n" +
                                $"User: {user}.\n" +
                                $"Message: {e.Message}\n" +
                                $"StackTrace: {e.StackTrace}.\n";
                _logger.Error(messageEx);
                Bot.SendTextMessageAsync(_logChannelId, messageEx);
            }

        }

        private void HandleMessage(Message message)
        {
            string user = GetUserOrGroupName(message);
            try
            {
                var chatId = message.Chat.Id;
                if (message.Text.StartsWith('/'))
                {
                    _testCommandHandler.Handle(message.Text, chatId);
                }
            }
            catch (Exception e)
            {
                var messageEx = $"HandleMessage Exception:\n" +
                                $"Command: \"{message.Text}\".\n" +
                                $"User: {user}.\n" +
                                $"Message: {e.Message}\n" +
                                $"StackTrace: {e.StackTrace}.\n";
                _logger.Error(messageEx);
                Bot.SendTextMessageAsync(_logChannelId, messageEx);
            }

        }

        private void HandleCallBack(Message message)
        {
            string user = GetUserOrGroupName(message);
            try
            {
                var chatId = message.Chat.Id;
                _testButtonHandler.Handle(message.Text, chatId);
            }
            catch (Exception e)
            {
                var messageEx = $"HandleCallBack Exception:\n" +
                                $"Command: \"{message.Text}\".\n" +
                                $"User: {user}.\n" +
                                $"Message: {e.Message}\n" +
                                $"StackTrace: {e.StackTrace}.\n";
                _logger.Error(messageEx);
                Bot.SendTextMessageAsync(_logChannelId, messageEx);
            }

        }

        private string GetUserOrGroupName(Message message)
        {
            if (!string.IsNullOrWhiteSpace(message.From.Username))
            {
                return "@" + message.From.Username;
            }
            if (!string.IsNullOrWhiteSpace(message.From.FirstName))
            {
                return message.From.FirstName + " " + message.From.LastName;
            }

            return message.Chat.Title;
        }

        private string BotResponse(Message message)
        {
            string userMessage = message.Text;
            string userName = GetUserOrGroupName(message);            
            if (userMessage.Contains("300") || userMessage.Contains("триста")) return $"{userName}, отсоси у тракториста!";
            else return "";
        }
    }

    public interface IBotClient
    {
    }
}
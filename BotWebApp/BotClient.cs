using System;
using System.Net;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace BotWebApp
{
    public class BotClient: IBotClient
    {
        //private static readonly object instanceLock = new object();

        //private static volatile BotClient instance;

        //public static BotClient Instance
        //{
        //    get
        //    {
        //        if (instance != null)
        //        {
        //            return instance;
        //        }
        //        lock (instanceLock)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new BotClient();
        //            }
        //        }
        //        return instance;
        //    }
        //}

        private readonly ILogger _logger;
        public ITelegramBotClient Bot { get; }

        private readonly string _logChannelId;

        public BotClient(ILogger logger)
        {
            _logger = logger;
            _logger.Info("BotClient is initialized");
            var configuration = ConfigurationModel.GetConfiguration();
            _logChannelId = configuration.TelegramConfiguration.LogChannelId;
            ////WebProxy proxy = new WebProxy(configuration.ProxyConfiguration.Host, configuration.ProxyConfiguration.Port);
            ////var wc = new WebClient();
            ////wc.Proxy = proxy;
            //// var resp = wc.DownloadString("http://www.holidaywebservice.com/HolidayService_v2/HolidayService2.asmx?wsdl");
            Bot = new TelegramBotClient(configuration.TelegramConfiguration.BotToken);
            var r = Bot.TestApiAsync().GetAwaiter().GetResult();
            _logger.Info($"Test bot request: {r}");
            Bot.OnMessage += ProcessMessage;
            Bot.OnCallbackQuery += BotOnOnCallbackQuery;
            Bot.StartReceiving();
        }


        private void BotOnOnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var query = callbackQueryEventArgs.CallbackQuery;
            try
            {
                _logger.Info("Bot OnOnCallbackQuery");
                query.Message.Text = query.Data;
                _logger.Info($"OnOnCallbackQuery message: \"{ query.Message?.Text}\"; chatId: { query.Message?.Chat.Id}");
                var resp = Bot.SendTextMessageAsync(new ChatId(_logChannelId),
                    $"OnOnCallbackQuery Command: \"{query.Message.Text}\". User: {query.Message.Chat.FirstName} {query.Message.Chat.LastName}.\n").GetAwaiter().GetResult();
                _logger.Info($"Logs channel message: \"{resp?.Text}\"; chatId: \"{resp?.Chat?.Id}\"");
                ProcessMessage(query.Message);
            }
            catch (Exception e)
            {
                var messageEx = $"OnOnCallbackQuery Exception:\n" +
                                $"Command: \"{query.Message.Text}\".\n" +
                                $"User: {query.Message.Chat.FirstName} {query.Message.Chat.LastName}.\n" +
                                $"Message: {e.Message}\n" +
                                $"StackTrace: {e.StackTrace}.\n";
                _logger.Error(messageEx);
                Bot.SendTextMessageAsync(_logChannelId, messageEx);
            }
        }

        private void ProcessMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            try
            {
                _logger.Info("Bot ProcessMessage");
                _logger.Info($"ProcessMessage message: \"{message.Text}\"; chatId: \"{message.Chat.Id}\"");
                var resp = Bot.SendTextMessageAsync(new ChatId(_logChannelId),
                    $"ProcessMessage Command: \"{message.Text}\". User: {message.Chat.FirstName} {message.Chat.LastName}.\n").GetAwaiter().GetResult();
                _logger.Info($"Logs channel message: \"{resp?.Text}\"; chatId: \"{resp?.Chat?.Id}\"");
                ProcessMessage(message);
            }
            catch (Exception e)
            {
                var messageEx = $"ProcessMessage Exception:\n" +
                                $"Command: \"{message.Text}\".\n" +
                                $"User: {message.Chat.FirstName} {message.Chat.LastName}.\n" +
                                $"Message: {e.Message}\n" +
                                $"StackTrace: {e.StackTrace}.\n";
                _logger.Error(messageEx);
                Bot.SendTextMessageAsync(_logChannelId, messageEx);
            }

        }

        private void ProcessMessage(Message message)
        {
            var chatId = message.Chat.Id;
        }
    }

    public interface IBotClient
    {
    }
}
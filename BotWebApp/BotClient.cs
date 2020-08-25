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
            _logger.Info("Bot OnOnCallbackQuery");
            var query = callbackQueryEventArgs.CallbackQuery;
            query.Message.Text = query.Data;
            _logger.Info($"OnOnCallbackQuery message: \"{ query.Message?.Text}\"; chatId: { query.Message?.Chat.Id}");
            ProcessMessage(query.Message);
        }

        private void ProcessMessage(object sender, MessageEventArgs messageEventArgs)
        {
            _logger.Info("Bot ProcessMessage");
            Message message = messageEventArgs.Message;
            _logger.Info($"ProcessMessage message: \"{message.Text}\"; chatId: \"{message.Chat.Id}\"");
            var resp = Bot.SendTextMessageAsync(new ChatId(_logChannelId), $"Command: \"{message.Text}\". User: {message.Chat.FirstName} {message.Chat.LastName}.\n").GetAwaiter().GetResult();
            _logger.Info($"Logs channel message: \"{resp?.Text}\"; chatId: \"{resp?.Chat?.Id}\"");
            try
            {
                ProcessMessage(message);
            }
            catch (Exception e)
            {
                Bot.SendTextMessageAsync(_logChannelId, $"Exception:\n" +
                                                        $"Command: \"{message.Text}\".\n" +
                                                        $"User: {message.Chat.FirstName} {message.Chat.LastName}.\n" +
                                                        $"Message: {e.Message}\n" +
                                                        $"StackTrace: {e.StackTrace}.\n");
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
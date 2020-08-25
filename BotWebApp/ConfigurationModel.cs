using System;
using System.IO;
using Newtonsoft.Json;

namespace BotWebApp
{
    public class ConfigurationModel
    {
        public ProxyConfiguration ProxyConfiguration { get; set; }

        public TelegramConfiguration TelegramConfiguration { get; set; }

        public static ConfigurationModel GetConfiguration()
        {
            var pathToConfigJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (File.Exists(pathToConfigJson))
            {
                var rawConfig = File.ReadAllText(pathToConfigJson);
                var config = JsonConvert.DeserializeObject<ConfigurationModel>(rawConfig, new JsonSerializerSettings() { Formatting = Formatting.Indented });
                return config;
            }
            throw new FileNotFoundException("config.json");
        }
    }

    public class ProxyConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }
    }

    public class TelegramConfiguration
    {
        public string BotToken { get; set; }

        public string LogChannelId { get; set; }
    }
}
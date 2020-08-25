using System.Collections.Generic;

namespace BotWebApp.Models
{
    public static class Commands
    {
        public static Dictionary<string, TestCommands> TestCommandsMap { get; set; } = new Dictionary<string, TestCommands>()
        {
            { "test_command_1", TestCommands.TestCommand1 }
        };
    }

    public enum TestCommands
    {
        None = 0,
        TestCommand1 = 1
    }
}
﻿using BotWebApp.Interfaces;
using BotWebApp.State.Airsoft;
using System.Collections.Generic;

namespace BotWebApp.Models
{
    public static class Commands
    {
        public static Dictionary<string, TestCommands> TestCommandsMap { get; set; } = new Dictionary<string, TestCommands>()
        {
            { "test_command_1", TestCommands.TestCommand1 }
        };
        public static Dictionary<string, ICommand> AirsoftCommandsMap { get; set; } = new Dictionary<string, ICommand>()
        {
            {"add", new StartCreationAirsoftEventState() }
        };
    }

    public enum TestCommands
    {
        None = 0,
        TestCommand1 = 1
    }

    public enum AirsoftCommands
    {
        None = 0,
        AddEvent = 1
    }

}
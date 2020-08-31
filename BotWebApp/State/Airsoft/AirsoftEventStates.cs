using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebApp.State.Airsoft
{
    public enum AirsoftEventStates
    {
        None = 0,
        StartCreation = 1,
        TitleInput = 2,
        DateInput = 3,
        CommentInput = 4,
        Finished = 5
    }
}

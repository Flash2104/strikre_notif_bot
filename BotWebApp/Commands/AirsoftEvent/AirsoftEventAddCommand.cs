using BotWebApp.Interfaces;
using BotWebApp.Models.Airsoft;
using BotWebApp.State.Airsoft;

namespace BotWebApp.Commands.AirsoftEvent
{
    public class AirsoftEventAddCommand : ICommand
    {
        private readonly IRepository<AirsoftEventModel> _repository;

        public AirsoftEventAddCommand(IRepository<AirsoftEventModel> repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            _repository.CreateAsync(new AirsoftEventModel()
            {
                State = AirsoftEventStates.StartCreation
            });
        }
    }
}

using MyApp.Application.Interfaces; // For ICommand

namespace MyApp.Application.Commands
{
    public class CreateGenderCommand : ICommand
    {
        public string Name { get; init; }

        public CreateGenderCommand(string name)
        {
            Name = name;
        }
    }
}

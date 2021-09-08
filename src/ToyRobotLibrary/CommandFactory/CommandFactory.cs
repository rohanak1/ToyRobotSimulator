using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.Configuration;

namespace ToyRobotLibrary.CommandFactory
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand GetCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return null;
            }

            var commandArguments = command.ToUpper().Split(' ');
            var tableTopDimensionsOptions = _serviceProvider.GetRequiredService<IOptions<TableTopDimensions>>();

            switch (commandArguments[0])
            {
                /*
                case "PLACE":
                {
                    var logger = _serviceProvider.GetRequiredService<ILogger<PlaceCommand>>();
                    var placeCommand = new PlaceCommand(logger, tableTopDimensionsOptions);
                    var placeCommandOptions = commandArguments[1].Split(',');
                    placeCommand.X = int.Parse(placeCommandOptions[0]);
                    placeCommand.Y = int.Parse(placeCommandOptions[1]);
                }*/

                case "MOVE":
                    return new MoveCommand(_serviceProvider.GetRequiredService<ILogger<MoveCommand>>(), tableTopDimensionsOptions);
                
                case "LEFT":
                    return new LeftRotationCommand(_serviceProvider.GetRequiredService<ILogger<LeftRotationCommand>>());

                case "RIGHT":
                    return new RightRotationCommand(_serviceProvider.GetRequiredService<ILogger<RightRotationCommand>>());
                
                case "REPORT":
                    return new ReportCommand(Console.Out, _serviceProvider.GetRequiredService<ILogger<ReportCommand>>());
                
                default:
                    return null;
            }
        }
    }
}

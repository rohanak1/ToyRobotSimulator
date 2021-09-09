using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.Robot;

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

            switch (commandArguments[0])
            {
                case "PLACE":
                {
                    if (commandArguments.Length != 2)
                    {
                        return null;
                    }
                    return Build(commandArguments[1]);
                }

                case "MOVE":
                    return _serviceProvider.GetRequiredService<MoveCommand>();
                
                case "LEFT":
                    return _serviceProvider.GetRequiredService<LeftRotationCommand>();

                case "RIGHT":
                    return _serviceProvider.GetRequiredService<RightRotationCommand>();
                
                case "REPORT":
                    return _serviceProvider.GetRequiredService<ReportCommand>();
                
                default:
                    return null;
            }
        }

        private PlaceCommand Build(string commandString)
        {
            var positionAndDirectionCommandPattern = new Regex(@"(?<xPosition>\d),(?<yPosition>\d),(?<direction>NORTH|EAST|WEST|SOUTH)");
            var positionAndDirectionCommandMatch = positionAndDirectionCommandPattern.Match(commandString);

            var placeCommand = _serviceProvider.GetRequiredService<PlaceCommand>();

            if (positionAndDirectionCommandMatch.Success)
            {
                Enum.TryParse(typeof(Direction), positionAndDirectionCommandMatch.Groups["direction"].Value,
                        ignoreCase: true,
                        out var result);

                placeCommand.X = int.Parse(positionAndDirectionCommandMatch.Groups["xPosition"].Value);
                placeCommand.Y = int.Parse(positionAndDirectionCommandMatch.Groups["yPosition"].Value);
                placeCommand.Direction = (Direction)result;

                return placeCommand;
            }

            var positionOnlyCommandPattern = new Regex(@"^(?<xPosition>\d),(?<yPosition>\d)$");
            var positionOnlyCommandMatch = positionOnlyCommandPattern.Match(commandString);
            if (positionOnlyCommandMatch.Success) 
            {
                placeCommand.X = int.Parse(positionOnlyCommandMatch.Groups["xPosition"].Value);
                placeCommand.Y = int.Parse(positionOnlyCommandMatch.Groups["yPosition"].Value);

                return placeCommand;
            }
            return null;
        }
    }
}

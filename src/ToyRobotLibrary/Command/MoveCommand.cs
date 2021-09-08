using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class MoveCommand : ICommand
    {
        private readonly ILogger<MoveCommand> _logger;
        private readonly TableTopDimensions _tableTopDimensions;

        public MoveCommand(ILogger<MoveCommand> logger, IOptions<TableTopDimensions> tableTopDimensions)
        {
            _logger = logger;
            _tableTopDimensions = tableTopDimensions.Value;
        }

        public void Execute(IRobot robot)
        {
            if (!robot.IsPlaced)
            {
                _logger.LogError("Ignoring command {Command} as robot has not been placed on board", "Move");
                throw new RobotNotPlacedException("Ignoring Move command as robot has not been placed on board");
            }

            var location = new Position
            {
                X = robot.Location.X,
                Y = robot.Location.Y
            };

            switch (robot.Direction)
            {
                case Direction.North:
                    location.Y++;
                    break;
                case Direction.West:
                    location.X--;
                    break;
                case Direction.South:
                    location.Y--;
                    break;
                case Direction.East:
                    location.X++;
                    break;
            }

            if (IsValidLocation(location))
            {
                robot.PlaceAt(location, robot.Direction);
            }
            else
            {
                _logger.LogCritical("Destructive {Command} to {@Location} not allowed", "Move", location);
                throw new InvalidMoveException("Destructive move - disallowed");
            }
        }

        private bool IsValidLocation(Position location) => location.X < _tableTopDimensions.X && location.X >= 0 &&
                   location.Y < _tableTopDimensions.Y && location.Y >= 0;
    }
}
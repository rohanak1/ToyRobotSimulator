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
                throw new InvalidRobotOperationException("Ignoring Move command as robot has not been placed on board");
            }

            var location = new Position
            {
                XCoordinate = robot.Location.XCoordinate,
                YCoordinate = robot.Location.YCoordinate
            };

            switch (robot.Direction)
            {
                case Direction.North:
                    location.YCoordinate++;
                    break;
                case Direction.West:
                    location.XCoordinate--;
                    break;
                case Direction.South:
                    location.YCoordinate--;
                    break;
                case Direction.East:
                    location.XCoordinate++;
                    break;
            }

            if (IsValidLocation(location))
            {
                robot.PlaceAt(location, robot.Direction);
            }
            else
            {
                _logger.LogCritical("Destructive {Command} to {@Location} not allowed", "Move", location);
                throw new InvalidRobotOperationException("Destructive move - disallowed");
            }
        }

        private bool IsValidLocation(Position location) => location.XCoordinate < _tableTopDimensions.X && location.XCoordinate >= 0 &&
                   location.YCoordinate < _tableTopDimensions.Y && location.YCoordinate >= 0;
    }
}
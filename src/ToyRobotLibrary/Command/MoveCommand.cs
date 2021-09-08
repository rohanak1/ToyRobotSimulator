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

        public string Type => "Move";

        public void Execute(IRobot robot)
        {
            if (!robot.IsPlaced)
            {
                _logger.LogError("Ignoring command {Command} as robot has not been placed on board", "Move");
                throw new InvalidRobotOperationException("Ignoring Move command as robot has not been placed on board");
            }

            var position = new Position
            {
                XCoordinate = robot.Position.XCoordinate,
                YCoordinate = robot.Position.YCoordinate
            };

            switch (robot.Direction)
            {
                case Direction.North:
                    position.YCoordinate++;
                    break;
                case Direction.West:
                    position.XCoordinate--;
                    break;
                case Direction.South:
                    position.YCoordinate--;
                    break;
                case Direction.East:
                    position.XCoordinate++;
                    break;
            }

            if (!IsValidPosition(position))
            {
                _logger.LogError("Destructive {Command} to {@Position} not allowed", "Move", position);
                throw new InvalidRobotOperationException("Destructive move - disallowed");                
            }

            robot.PlaceAt(position, robot.Direction);
        }

        private bool IsValidPosition(Position position) => position.XCoordinate < _tableTopDimensions.X && position.XCoordinate >= 0 &&
                   position.YCoordinate < _tableTopDimensions.Y && position.YCoordinate >= 0;
    }
}
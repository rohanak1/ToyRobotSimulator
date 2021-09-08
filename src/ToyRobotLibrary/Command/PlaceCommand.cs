using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class PlaceCommand : ICommand
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public Direction? Direction { get; set; }
        private ILogger<PlaceCommand> _logger;
        private TableTopDimensions _tableTopDimensions;

        public PlaceCommand(ILogger<PlaceCommand> logger, IOptions<TableTopDimensions> tableTopDimensions)
        {
            _logger = logger;
            _tableTopDimensions = tableTopDimensions.Value;
        }

        public string Type => "Place";

        public void Execute(IRobot robot)
        {
            var position = new Position { XCoordinate = X, YCoordinate = Y};

            if (!IsValidPosition(position))
            {
                _logger.LogError("Destructive {Command} to {@Position} not allowed", "Place", position);
                throw new InvalidRobotOperationException($"Destructive place to {X}, {Y} disallowed");                
            }

            if (!robot.IsPlaced && Direction == null)
            {
                var errorMessage = "Position and Direction to be provided as robot is not yet placed on board";
                _logger.LogError(errorMessage);
                throw new InvalidRobotOperationException(errorMessage);
            }

            robot.PlaceAt(position, Direction ?? robot.Direction);
        }

        private bool IsValidPosition(Position position) => position.XCoordinate < _tableTopDimensions.X && position.XCoordinate >= 0 &&
                   position.YCoordinate < _tableTopDimensions.Y && position.YCoordinate >= 0;
    }
}

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class PlaceCommand : ICommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction? Direction { get; set; }
        private ILogger<PlaceCommand> _logger;
        private TableTopDimensions _tableTopDimensions;

        public PlaceCommand(ILogger<PlaceCommand> logger, IOptions<TableTopDimensions> tableTopDimensions)
        {
            _logger = logger;
            _tableTopDimensions = tableTopDimensions.Value;
        }

        public void Execute(IRobot robot)
        {
            if (!robot.IsPlaced && Direction == null)
            {
                var errorMessage = "Position and Direction to be provided as robot is not yet placed on board";
                _logger.LogCritical(errorMessage);
                throw new InvalidRobotOperationException(errorMessage);
            }

            robot.PlaceAt(new Position { XCoordinate = X, YCoordinate = Y}, Direction ?? robot.Direction);
        }
    }
}

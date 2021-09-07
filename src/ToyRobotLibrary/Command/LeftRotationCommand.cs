using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class LeftRotationCommand : ICommand
    {
        private readonly ILogger<LeftRotationCommand> _logger;

        public LeftRotationCommand(ILogger<LeftRotationCommand> logger)
        {
            _logger = logger;
        }

        public void Execute(IRobot robot)
        {
            if (!robot.IsPlaced)
            {
                _logger.LogError("Ignoring command {Command} as robot has not been placed on board", "LeftRotate");
                throw new RobotNotPlacedException("Ignoring LeftRotate command as robot has not been placed on board");
            }

            var newDirection = robot.Direction;

            switch (robot.Direction)
            {
                case Direction.North:
                    newDirection = Direction.West;
                    break;

                case Direction.South:
                    newDirection = Direction.East;
                    break;
                
                case Direction.East:
                    newDirection = Direction.North;
                    break;

                case Direction.West:
                    newDirection = Direction.South;
                    break;
            }

            robot.PlaceAt(robot.Location, newDirection);
        }
    }
}

using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class RightRotationCommand : ICommand
    {
        private readonly ILogger<RightRotationCommand> _logger;

        public RightRotationCommand(ILogger<RightRotationCommand> logger)
        {
            _logger = logger;
        }

        public string Type => "Right-Rotation";

        public void Execute(IRobot robot)
        {
            if (!robot.IsPlaced)
            {
                _logger.LogError("Ignoring command {Command} as robot has not been placed on board", "RightRotate");
                throw new InvalidRobotOperationException("Ignoring RightRotate command as robot has not been placed on board");
            }

            var newDirection = robot.Direction;

            switch (robot.Direction)
            {
                case Direction.North:
                    newDirection = Direction.East;
                    break;

                case Direction.East:
                    newDirection = Direction.South;
                    break;

                case Direction.West:
                    newDirection = Direction.North;
                    break;

                case Direction.South:
                    newDirection = Direction.West;
                    break;
            }

            robot.PlaceAt(robot.Position, newDirection);
        }
    }
}

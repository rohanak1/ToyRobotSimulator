using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class RightRotationCommand : ICommand
    {
        public void Execute(IRobot robot)
        {
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

            robot.PlaceAt(robot.Location, newDirection);
        }
    }
}

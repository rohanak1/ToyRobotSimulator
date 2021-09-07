using System;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class LeftRotationCommand : ICommand
    {
        public void Execute(IRobot robot)
        {
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

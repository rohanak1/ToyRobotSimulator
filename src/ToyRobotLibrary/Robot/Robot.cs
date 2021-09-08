using System;

namespace ToyRobotLibrary.Robot
{
    public class Robot : IRobot
    {
        public Position Position { get; private set; }

        public Direction Direction { get; private set; }

        public bool IsPlaced { get; private set; }

        public void PlaceAt(Position position, Direction direction)
        {

        }
    }
}

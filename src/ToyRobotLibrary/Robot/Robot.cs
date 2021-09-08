using System;

namespace ToyRobotLibrary.Robot
{
    public class Robot : IRobot
    {
        public Position Location { get; private set; }

        public Direction Direction { get; private set; }

        public bool IsPlaced { get; private set; }

        public void PlaceAt(Position location, Direction direction)
        {

        }
    }
}

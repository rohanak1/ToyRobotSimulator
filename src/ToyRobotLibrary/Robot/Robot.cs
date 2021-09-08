using System;

namespace ToyRobotLibrary.Robot
{
    public class Robot : IRobot
    {
        public Position Location => throw new NotImplementedException();

        public Direction Direction => throw new NotImplementedException();

        public bool IsPlaced => throw new NotImplementedException();

        public void PlaceAt(Position location, Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}

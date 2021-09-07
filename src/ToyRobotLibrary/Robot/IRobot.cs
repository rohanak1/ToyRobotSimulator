using System;

namespace ToyRobotLibrary.Robot
{
    public interface IRobot
    {
        public Location Location { get; }
        public Direction Direction { get;}
        public void PlaceAt(Location location, Direction direction);
    }
}

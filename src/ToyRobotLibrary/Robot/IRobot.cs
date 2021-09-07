namespace ToyRobotLibrary.Robot
{
    public interface IRobot
    {
        public Location Location { get; }
        public Direction Direction { get;}
        public bool IsPlaced { get; }
        public void PlaceAt(Location location, Direction direction);
    }
}

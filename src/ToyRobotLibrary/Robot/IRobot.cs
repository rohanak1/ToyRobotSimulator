namespace ToyRobotLibrary.Robot
{
    public interface IRobot
    {
        public Position Position { get; }
        public Direction Direction { get;}
        public bool IsPlaced { get; }
        public void PlaceAt(Position position, Direction direction);
    }
}

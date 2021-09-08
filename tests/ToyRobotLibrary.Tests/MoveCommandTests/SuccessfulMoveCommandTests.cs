using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Robot;
using Xunit;

namespace ToyRobotLibrary.Tests.MoveCommandTests
{
    public class SuccessfulMoveCommandTests
    {
        const int CurrentXCoordinate = 3;
        const int CurrentYCoordinate = 3;
        const int MaxXCoordinate = 5;
        const int MaxYCoordinate = 5;

        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly Mock<IOptions<TableTopDimensions>> _tableTopDimensionsMock = new Mock<IOptions<TableTopDimensions>>();
        private readonly MoveCommand _moveCommand;

        public SuccessfulMoveCommandTests()
        {
            _tableTopDimensionsMock.Setup(t => t.Value).Returns(new TableTopDimensions
            {
                X = MaxXCoordinate,
                Y = MaxYCoordinate
            });

            _moveCommand = new MoveCommand(
                new Mock<ILogger<MoveCommand>>().Object,
                _tableTopDimensionsMock.Object);

            _robotMock.Setup(r => r.IsPlaced).Returns(true);
            _robotMock.Setup(r => r.Location).Returns(new Position
            {
                XCoordinate = CurrentXCoordinate,
                YCoordinate = CurrentYCoordinate
            });
        }

        [Fact]
        public void GivenNorthFacingRobot_WhenMoved_ThenRobotMovesOneUnitNorth()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.North);

            // Act
            _moveCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.Is<Position>(l => l.XCoordinate == CurrentXCoordinate && l.YCoordinate == CurrentYCoordinate + 1), It.Is<Direction>(d => d == Direction.North)), "Y position should increment by 1");
        }

        [Fact]
        public void GivenSouthFacingRobot_WhenMoved_ThenRobotMovesOneUnitSouth()
        {
            // Arrange
            const Direction currentDirection = Direction.South;
            _robotMock.Setup(r => r.Direction).Returns(currentDirection);

            // Act
            _moveCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.Is<Position>(l => l.XCoordinate == CurrentXCoordinate && l.YCoordinate == CurrentYCoordinate - 1), It.Is<Direction>(d => d == currentDirection)), "Y position should decrement by 1");
        }

        [Fact]
        public void GivenEastFacingRobot_WhenMoved_ThenRobotMovesOneUnitEast()
        {
            // Arrange
            const Direction currentDirection = Direction.East;
            _robotMock.Setup(r => r.Direction).Returns(currentDirection);

            // Act
            _moveCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.Is<Position>(l => l.XCoordinate == CurrentXCoordinate + 1 && l.YCoordinate == CurrentYCoordinate), It.Is<Direction>(d => d == currentDirection)), "X position should increment by 1");
        }

        [Fact]
        public void GivenWestFacingRobot_WhenMoved_ThenRobotMovesOneUnitWest()
        {
            // Arrange
            const Direction currentDirection = Direction.West;
            _robotMock.Setup(r => r.Direction).Returns(currentDirection);

            // Act
            _moveCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.Is<Position>(l => l.XCoordinate == CurrentXCoordinate - 1 && l.YCoordinate == CurrentYCoordinate), It.Is<Direction>(d => d == currentDirection)), "X position should decrement by 1");
        }
    }
}

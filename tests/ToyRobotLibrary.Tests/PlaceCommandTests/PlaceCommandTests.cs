using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;
using Xunit;

namespace ToyRobotLibrary.Tests.PlaceCommandTests
{
    public class PlaceCommandTests
    {
        const int MaxXCoordinate = 5;
        const int MaxYCoordinate = 5;
        const int ExpectedXCoordinate = 3;
        const int ExpectedYCoordinate = 3;
        const Direction ExpectedDirection = Direction.North;

        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly PlaceCommand _placeCommand;
        private Mock<IOptions<TableTopDimensions>> _tableTopDimensionsMock = new Mock<IOptions<TableTopDimensions>>();

        public PlaceCommandTests()
        {
            _tableTopDimensionsMock.Setup(t => t.Value).Returns(new TableTopDimensions
            {
                X = MaxXCoordinate,
                Y = MaxYCoordinate
            });

            _placeCommand = new PlaceCommand(
                new Mock<ILogger<PlaceCommand>>().Object,
                _tableTopDimensionsMock.Object);
                
        }

        [Fact]
        public void GivenAnUnplacedRobot_WhenPlacedWithPositionAndDirection_ThenRobotShouldBeAtNewPosition()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(false);

            // Act
            _placeCommand.X = ExpectedXCoordinate;
            _placeCommand.Y = ExpectedYCoordinate;
            _placeCommand.Direction = ExpectedDirection;

            _placeCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.Is<Position>(p => p.XCoordinate == ExpectedXCoordinate && p.YCoordinate == ExpectedYCoordinate), It.Is<Direction>(d => d == ExpectedDirection)), "Unplaced robot should be placed at new position when position and direction are provided");
        }

        [Fact]
        public void GivenAnUnplacedRobot_WhenPlacedWithPositionAndNoDirection_ThenRobotIsNotPlaced()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(false);

            // Act
            _placeCommand.X = ExpectedXCoordinate;
            _placeCommand.Y = ExpectedYCoordinate;

            var ex = Assert.Throws<InvalidRobotOperationException>(() => _placeCommand.Execute(_robotMock.Object));

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.IsAny<Direction>()), Times.Never, "An unplaced robot should be given a position and direction to place it");
        }

        [Fact]
        public void GivenAPlacedRobot_WhenPlacedWithOnlyPosition_ThenRobotIsPlacedAtNewPosition_WithSameDirection()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(true);
            _robotMock.Setup(r => r.Direction).Returns(Direction.North);
            _placeCommand.X = ExpectedXCoordinate;
            _placeCommand.Y = ExpectedYCoordinate;

            // Act
            _placeCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(
                It.Is<Position>(p => p.XCoordinate == ExpectedXCoordinate && p.YCoordinate == ExpectedYCoordinate),
                It.Is<Direction>(d => d == Direction.North)),
                "Robot should be placed at new position with no change in direction");
        }
    }
}

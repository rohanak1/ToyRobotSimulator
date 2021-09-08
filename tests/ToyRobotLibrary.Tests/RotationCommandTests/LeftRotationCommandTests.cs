using Xunit;
using Moq;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Command;
using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Exceptions;

namespace ToyRobotLibrary.Tests.RotationCommandTests
{
    public class LeftRotationCommandTests
    {
        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly LeftRotationCommand _leftRotationCommand = new LeftRotationCommand(new Mock<ILogger<LeftRotationCommand>>().Object);

        public LeftRotationCommandTests()
        {
            _robotMock.Setup(r => r.IsPlaced).Returns(true);
        }

        [Fact]
        public void GivenANorthFacingRobot_WhenRotatedLeft_ThenRobotFacesWest()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.North);

            // Act
            _leftRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.West)), "North facing robot should face west on left rotation");
        }

        [Fact]
        public void GivenSouthFacingRobot_WhenRotatedLeft_ThenRobotFacesEast()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.South);

            // Act
            _leftRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.East)), "South facing robot should face east on left rotation");
        }

        [Fact]
        public void GivenEastFacingRobot_WhenRotatedLeft_ThenRobotFacesNorth()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.East);

            // Act
            _leftRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.North)), "East facing robot should face north on left rotation");
        }

        [Fact]
        public void GivenWestFacingRobot_WhenRotatedLeft_ThenRobotFacesSouth()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.West);

            // Act
            _leftRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.South)), "West facing robot should face south on left rotation");
        }

        [Fact]
        public void GivenARobotNotPlacedOnBoard_WhenRotatedLeft_ThenRobotDoesNotRotate()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(false);

            // Act
            var ex = Assert.Throws<InvalidRobotOperationException>(() => _leftRotationCommand.Execute(_robotMock.Object));

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.IsAny<Direction>()), Times.Never, "Left rotation should not be performed as robot is not placed");
        }
    }
}
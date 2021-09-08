using Xunit;
using Moq;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Command;
using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Exceptions;

namespace ToyRobotLibrary.Tests.RotationCommandTests
{
    public class RightRotationCommandTests
    {
        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly RightRotationCommand _rightRotationCommand = new RightRotationCommand(new Mock<ILogger<RightRotationCommand>>().Object);

        public RightRotationCommandTests()
        {
            _robotMock.Setup(r => r.IsPlaced).Returns(true);
        }

        [Fact]
        public void GivenANorthFacingRobot_WhenRotatedRight_ThenRobotFacesEast()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.North);

            // Act
            _rightRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.East)), "North facing robot should face east on right rotation");
        }

        [Fact]
        public void GivenEastFacingRobot_WhenRotatedRight_ThenRobotFacesSouth()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.East);

            // Act
            _rightRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.South)), "East facing robot should face south on right rotation");
        }

        [Fact]
        public void GivenWestFacingRobot_WhenRotatedRight_ThenRobotFacesNorth()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.West);

            // Act
            _rightRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.North)), "West facing robot should face north on right rotation");
        }

        [Fact]
        public void GivenSouthFacingRobot_WhenRotatedRight_ThenRobotFacesWest()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.South);

            // Act
            _rightRotationCommand.Execute(_robotMock.Object);

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.Is<Direction>(d => d == Direction.West)), "South facing robot should face west on right rotation");
        }

        [Fact]
        public void GivenARobotNotPlacedOnBoard_WhenRotatedRight_ThenRobotDoesNotRotate()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(false);

            // Act
            var ex = Assert.Throws<RobotNotPlacedException>(() => _rightRotationCommand.Execute(_robotMock.Object));

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.IsAny<Direction>()), Times.Never, "Right rotation should not be performed as robot is not placed");
        }
    }
}

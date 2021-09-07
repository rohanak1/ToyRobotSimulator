using Xunit;
using Moq;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Command;

namespace ToyRobotLibrary.Tests.RotationCommandTests
{
    public class LeftRotationCommandTests
    {
        private readonly Mock<IRobot> robotMock = new Mock<IRobot>();
        private readonly LeftRotationCommand _leftRotationCommand = new LeftRotationCommand();

        [Fact]
        public void GivenANorthFacingRobot_WhenRotatedLeft_ThenRobotFacesWest()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.North);

            // Act
            _leftRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.West)), "North facing robot should face west on left rotation");
        }

        [Fact]
        public void GivenSouthFacingRobot_WhenRotatedLeft_ThenRobotFacesEast()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.South);

            // Act
            _leftRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.East)), "South facing robot should face east on left rotation");
        }

        [Fact]
        public void GivenEastFacingRobot_WhenRotatedLeft_ThenRobotFacesNorth()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.East);

            // Act
            _leftRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.North)), "East facing robot should face north on left rotation");
        }

        [Fact]
        public void GivenWestFacingRobot_WhenRotatedLeft_ThenRobotFacesSouth()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.West);

            // Act
            _leftRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.South)), "West facing robot should face south on left rotation");
        }
    }
}
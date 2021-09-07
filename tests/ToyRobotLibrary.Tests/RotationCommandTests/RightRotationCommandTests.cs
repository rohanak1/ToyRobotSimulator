using Xunit;
using Moq;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Command;

namespace ToyRobotLibrary.Tests.RotationCommandTests
{
    public class RightRotationCommandTests
    {
        private readonly Mock<IRobot> robotMock = new Mock<IRobot>();
        private readonly RightRotationCommand _rightRotationCommand = new RightRotationCommand();

         [Fact]
        public void GivenANorthFacingRobot_WhenRotatedRight_ThenRobotFacesEast()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.North);

            // Act
            _rightRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.East)), "North facing robot should face east on right rotation");
        }

        [Fact]
        public void GivenEastFacingRobot_WhenRotatedRight_ThenRobotFacesSouth()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.East);

            // Act
            _rightRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.South)), "East facing robot should face south on right rotation");
        }

        [Fact]
        public void GivenWestFacingRobot_WhenRotatedRight_ThenRobotFacesNorth()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.West);

            // Act
            _rightRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.North)), "West facing robot should face north on right rotation");
        }

        [Fact]
        public void GivenSouthFacingRobot_WhenRotatedRight_ThenRobotFacesWest()
        {
            // Arrange
            robotMock.Setup(r => r.Direction).Returns(Direction.South);

            // Act
            _rightRotationCommand.Execute(robotMock.Object);

            // Assert
            robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.Is<Direction>(d => d == Direction.West)), "South facing robot should face west on right rotation");
        }
    }
}

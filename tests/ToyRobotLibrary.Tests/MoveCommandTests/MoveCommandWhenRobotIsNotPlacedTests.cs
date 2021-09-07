using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;
using Xunit;

namespace ToyRobotLibrary.Tests.MoveCommandTests
{
    public class MoveCommandWhenRobotIsNotPlacedTests
    {
        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly MoveCommand _moveCommand = new MoveCommand(
                new Mock<ILogger<MoveCommand>>().Object,
                new Mock<IOptions<TableTopDimensions>>().Object);

        [Fact]
        public void GivenRobotWhichIsNotPlaced_WhenMoved_ThenRobotDoesNotMove()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(false);

            // Act
            var ex = Assert.Throws<RobotNotPlacedException>(() => _moveCommand.Execute(_robotMock.Object));

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Location>(), It.IsAny<Direction>()), Times.Never, "Unplaced robot should not be moved");
        }
    }
}

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
    public class OutOfBoundsMoveCommandTests
    {
        const int MaxXCoordinate = 6;
        const int MaxYCoordinate = 6;

        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly Mock<IOptions<TableTopDimensions>> _tableTopDimensionsMock = new Mock<IOptions<TableTopDimensions>>();
        private readonly MoveCommand _moveCommand;

        public OutOfBoundsMoveCommandTests()
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

        }

        [Fact]
        public void GivenRobotAtNorthEastCornerOfBoard_WhenMoved_ThenCommandIsRejected()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.North);
            _robotMock.Setup(r => r.Position).Returns(new Position
            {
                XCoordinate = 5,
                YCoordinate = 5
            });

            // Act
            var ex = Assert.Throws<InvalidRobotOperationException>(() => _moveCommand.Execute(_robotMock.Object));

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.IsAny<Direction>()), Times.Never, "Out of bounds move is not allowed");
        }

        [Fact]
        public void GivenRobotAtSouthWestCornerOfBoard_WhenMoved_ThenCommandIsRejected()
        {
            // Arrange
            _robotMock.Setup(r => r.Direction).Returns(Direction.South);
            _robotMock.Setup(r => r.Position).Returns(new Position
            {
                XCoordinate = 0,
                YCoordinate = 0
            });

            // Act
            var ex = Assert.Throws<InvalidRobotOperationException>(() => _moveCommand.Execute(_robotMock.Object));

            // Assert
            _robotMock.Verify(r => r.PlaceAt(It.IsAny<Position>(), It.IsAny<Direction>()), Times.Never, "Out of bounds move is not allowed");
        }
    }
}

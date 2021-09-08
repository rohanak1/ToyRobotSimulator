using System.IO;
using Microsoft.Extensions.Logging;
using Moq;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.Robot;
using Xunit;

namespace ToyRobotLibrary.Tests.ReportCommandTests
{
    public class ReportCommandTests
    {
        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private readonly ReportCommand _reportCommand;
        private readonly Mock<TextWriter> _writerMock = new Mock<TextWriter>();

        public ReportCommandTests()
        {
            _reportCommand = new ReportCommand(
                _writerMock.Object,
                new Mock<ILogger<ReportCommand>>().Object);
        }

        [Fact]
        public void GivenAPlacedRobot_WhenReportCommandIsExecuted_ThenCurrentPositionAndDirectionAreReported()
        {
            const int ExpectedXCoordinate = 3;
            const int ExpectedYCoordinate = 3;
            const Direction ExpectedDirection = Direction.North;

            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(true);
            _robotMock.Setup(r => r.Position).Returns(new Position
            {
                XCoordinate = ExpectedXCoordinate,
                YCoordinate = ExpectedYCoordinate
            });
            _robotMock.Setup(r => r.Direction).Returns(ExpectedDirection);

            // Act
            _reportCommand.Execute(_robotMock.Object);

            // Assert
            _writerMock.Verify(w => w.WriteLine(It.Is<string>(s =>s == $"Output: {ExpectedXCoordinate},{ExpectedYCoordinate},{ExpectedDirection}")));
        }

        [Fact]
        public void GivenAnUnplacedRobot_WhenReportCommandIsExecuted_ThenUnplacedMessageIsReported()
        {
            // Arrange
            _robotMock.Setup(r => r.IsPlaced).Returns(false);

            // Act
            _reportCommand.Execute(_robotMock.Object);

            // Assert
            _writerMock.Verify(w => w.WriteLine(It.Is<string>(s => s == "Robot is not yet placed on board")));
        }
    }
}

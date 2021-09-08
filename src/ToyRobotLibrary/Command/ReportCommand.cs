using System.IO;
using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class ReportCommand : ICommand
    {
        private readonly TextWriter _textWriter;
        private readonly ILogger<ReportCommand> _logger;

        public ReportCommand(
            TextWriter textWriter,
            ILogger<ReportCommand> logger)
        {
            _textWriter = textWriter;
            _logger = logger;
        }

        public string Type => "Report";

        public void Execute(IRobot robot)
        {
            if (robot.IsPlaced)
            {
                var robotPosition = $"{robot.Position.XCoordinate},{robot.Position.YCoordinate},{robot.Direction}";
                _textWriter.WriteLine(robotPosition);
            }
            else
            {
                _textWriter.WriteLine("Robot is not yet placed on board");
            }
        }
    }
}

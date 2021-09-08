using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public class ReportCommand : ICommand
    {
        private readonly TextWriter _textWriter;
        private readonly ILogger<ReportCommand> _logger;
        private readonly TableTopDimensions _tableTopDimensions;

        public ReportCommand(
            TextWriter textWriter,
            ILogger<ReportCommand> logger,
            IOptions<TableTopDimensions> tableTopDimensions)
        {
            _textWriter = textWriter;
            _logger = logger;
            _tableTopDimensions = tableTopDimensions.Value;
        }

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

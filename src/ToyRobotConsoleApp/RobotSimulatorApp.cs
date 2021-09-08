using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.CommandFactory;
using ToyRobotLibrary.Exceptions;
using ToyRobotLibrary.Robot;

namespace ToyRobotConsoleApp
{
    public class RobotSimulatorApp : IHostedService
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IRobot _robot;
        private readonly ILogger<RobotSimulatorApp> _logger;

        public RobotSimulatorApp(
            ICommandFactory commandFactory,
            IRobot robot,
            ILogger<RobotSimulatorApp> logger)
        {
            _commandFactory = commandFactory;
            _robot = robot;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Toy Robot simulator");
            Console.WriteLine("========================");
            Console.WriteLine("Available commands ->");
            Console.WriteLine("PLACE X,Y,DIRECTION");
            Console.WriteLine("MOVE");
            Console.WriteLine("LEFT");
            Console.WriteLine("RIGHT");
            Console.WriteLine("REPORT");
            Console.WriteLine("Ctrl-C to exit application");

            CommandExecutor();
            return Task.CompletedTask;
        }

        private void CommandExecutor()
        {
            while (true)
            {
                ICommand command = null;

                try
                {
                    var input = Console.ReadLine();
                    command = _commandFactory.GetCommand(input);
                    command.Execute(_robot);
                }
                catch (InvalidRobotOperationException ex)
                {
                    _logger.LogError(ex, "Command {Command} failed", command.Type);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Thank you for using Toy Robot Simulator");
            return Task.CompletedTask;
        }
    }
}

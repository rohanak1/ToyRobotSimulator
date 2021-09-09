using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToyRobotLibrary.Command;
using ToyRobotLibrary.CommandFactory;
using ToyRobotLibrary.Configuration;
using ToyRobotLibrary.Robot;

namespace ToyRobotConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IRobot, Robot>();
                    services.AddTransient<ICommandFactory, CommandFactory>();
                    services.Configure<TableTopDimensions>(hostContext.Configuration.GetSection("TableTopDimensions"));
                    services.AddHostedService<RobotSimulatorApp>();
                    services.AddTransient<TextWriter>(sp => Console.Out);
                    services.AddTransient<MoveCommand>();
                    services.AddTransient<LeftRotationCommand>();
                    services.AddTransient<RightRotationCommand>();
                    services.AddTransient<ReportCommand>();
                    services.AddTransient<PlaceCommand>();
                })
                .UseConsoleLifetime();
        }
}

using System;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public interface ICommand
    {
        public string Type { get; }
        public void Execute(IRobot robot);
    }
}

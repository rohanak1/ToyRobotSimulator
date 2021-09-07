using System;
using ToyRobotLibrary.Robot;

namespace ToyRobotLibrary.Command
{
    public interface ICommand
    {
        public void Execute(IRobot robot);
    }
}

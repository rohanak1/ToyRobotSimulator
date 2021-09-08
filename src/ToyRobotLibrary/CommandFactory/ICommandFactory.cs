using ToyRobotLibrary.Command;

namespace ToyRobotLibrary.CommandFactory
{
    public interface ICommandFactory
    {
        public ICommand GetCommand(string command);
    }
}

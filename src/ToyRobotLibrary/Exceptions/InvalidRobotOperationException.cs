using System;

namespace ToyRobotLibrary.Exceptions
{
    public class InvalidRobotOperationException : Exception
    {
        public InvalidRobotOperationException()
        {
        }

        public InvalidRobotOperationException(string message)
            : base(message)
        {
        }

        public InvalidRobotOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

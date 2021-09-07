using System;

namespace ToyRobotLibrary.Exceptions
{
    public class RobotNotPlacedException : Exception
    {
        public RobotNotPlacedException()
        {
        }

        public RobotNotPlacedException(string message)
            : base(message)
        {
        }

        public RobotNotPlacedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

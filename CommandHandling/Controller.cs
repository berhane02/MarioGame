using System.Collections.Generic;

namespace MarioGame.CommandHandling
{
    public abstract class Controller : IController
    {
        protected Dictionary<int, ICommand> Commands { get; }

        protected Controller()
        {
            Commands = new Dictionary<int, ICommand>();
        }
        public void Command(int key, ICommand value)
        {
            Commands.Add(key, value);
        }
        public abstract void UpdateInput();
    }
}

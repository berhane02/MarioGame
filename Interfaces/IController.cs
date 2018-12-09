using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.CommandHandling
{
    interface IController
    {
        void Command(int key, ICommand value);
        void UpdateInput();
    }
}

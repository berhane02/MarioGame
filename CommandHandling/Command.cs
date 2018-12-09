using System.Runtime.Remoting.Channels;
using MarioGame.Block;
using MarioGame.Entities;
using MarioGame.Mario;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MarioGame.CommandHandling
{
        public interface ICommand
        {
            void Execute();
        }

    public abstract class MarioCommand : ICommand
    {
        public MarioEntity Receiver { get; set; }

        protected MarioCommand(MarioEntity receiver)
        {
            this.Receiver = receiver;
        }
        public abstract void Execute();
    }

    public class BlockCommand : ICommand
    {
        public BlockEntity Receiver { get; set; }

        public BlockCommand(BlockEntity receiver)
        {
            Receiver = receiver;        
        }

        public void Execute()
        {
            Receiver.BumpTransition();
        }

    }

    public class CollisionCommand : ICommand
    {
        protected Level Receiver { get; set; }
        public CollisionCommand(Level receiver)
        {
            Receiver = receiver;
        }
        public void Execute()
        {
            Receiver.ToggleCommand();
        }
      
    }

    public class ResetCommand : ICommand
    {
        protected Game1 Receiver { get; set; }
      public ResetCommand(Game1 receiver)
        {
            Receiver = receiver;
        }
        public void Execute()
        {
            Receiver.Reset();
        }
    }

    public class MuteCommand : ICommand
    {
        protected Level Receiver;
        public MuteCommand(Level receiver)
        {
            Receiver = receiver;
        }
        public void Execute()
        {
           Receiver.Mute();
        }
    }

    public class PauseCommand : ICommand
    {
        protected Game1 Receiver;
        public PauseCommand(Game1 receiver)
        {
            Receiver = receiver;
        }
        public void Execute()
        {
            Receiver.Pause();
        }
    }


}

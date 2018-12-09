using MarioGame.Mario;
using MarioGame.Block;

namespace MarioGame.CommandHandling
{
    class ExitCommand : ICommand
    {
        protected Game1 Receiver;

        public ExitCommand(Game1 receiver)
        {
            Receiver = receiver;
        }
        public void Execute()
        {
            Receiver.ExitCommand();
        }
    }

    /* All action commands*/
    public class IdleCommand: MarioCommand
    {
        public IdleCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Left();

        }
    }
    class LeftCommand : MarioCommand
    {
        public LeftCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Left();
            
        }
    }
    class RightCommand : MarioCommand
    {
        public RightCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Right();
            

        }
    }
    class UpCommand : MarioCommand
    {
        public UpCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Up();
            
        }
    }
    class DownCommand : MarioCommand
    {
        public DownCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Down();
          
        }
    }

    /* All Cheat Codes*/
    class DamageCommand : MarioCommand
    {
        public DamageCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.TakeDamage();
           
        }
    }
    class StandardCommand : MarioCommand
    {
        public StandardCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Standard();
           
        }
    }
    class SuperCommand : MarioCommand
    {
        public SuperCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Super();
           
        }
    }
    class FireCommand : MarioCommand
    {
        public FireCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Fire();           
        }
    }

    class InvincibleCommand : MarioCommand
    {
        public InvincibleCommand(MarioEntity receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            Receiver.Invincible();
        }
    }

}

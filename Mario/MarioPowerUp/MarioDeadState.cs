using MarioGame.Levels;

namespace MarioGame.Mario.MarioPowerUp
{
    class MarioDeadState : MarioPowerUpState
    {
        public MarioDeadState(MarioEntity mario) : base(mario)
        {
            if (mario.Live > 1)
            {
                Mario.Sounds.PlaySound(EventSoundEffects.EventSounds.Die);
            }
            mario.HasDied = true;
        }
        public override void FireTransition()
        {
            CurrentState = new MarioFireState(Mario);
            CurrentState.Enter(this);
        }

        public override void StandardTransition()
        {
            CurrentState = new MarioStandardState(Mario);
            CurrentState.Enter(this);
        }

        public override void SuperTransition()
        {
            CurrentState = new MarioSuperState(Mario);
            CurrentState.Enter(this);
        }

    }
}

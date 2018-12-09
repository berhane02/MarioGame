using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MarioGame.Levels
{
    public class EventSoundEffects
    {
        public enum EventSounds

        {Jump, SuperJump, Stomp, Die, CollectCoin, PowerUp, CollectPowerUp, Collect1Up,
        BrickBump, BrickBreak, PipeTravel, TimeWarning, GameOver, BooSound
        }

        private Dictionary<EventSounds, SoundEffect> SoundBank;
        private float _volume;
        public EventSoundEffects(ContentManager content)
        {
            _volume = 1.0f;
            SoundBank = new Dictionary<EventSounds, SoundEffect>();
            SoundEffect jump = content.Load<SoundEffect>("Sounds/smb_jumpsmall");
            SoundBank.Add(EventSounds.Jump, jump);
            SoundEffect superJump = content.Load<SoundEffect>("Sounds/smb_jump-super");
            SoundBank.Add(EventSounds.SuperJump, superJump);
            SoundEffect stomp = content.Load<SoundEffect>("Sounds/smb_stomp");
            SoundBank.Add(EventSounds.Stomp, stomp);
            SoundEffect die = content.Load<SoundEffect>("Sounds/smb_mariodie");
            SoundBank.Add(EventSounds.Die, die);
            SoundEffect collectCoin = content.Load<SoundEffect>("Sounds/smb_coin");
            SoundBank.Add(EventSounds.CollectCoin, collectCoin);
            SoundEffect powerUp = content.Load<SoundEffect>("Sounds/smb_powerup_appears");
            SoundBank.Add(EventSounds.PowerUp, powerUp);
            SoundEffect collectPowerUp = content.Load<SoundEffect>("Sounds/smb_powerup");
            SoundBank.Add(EventSounds.CollectPowerUp, collectPowerUp);
            SoundEffect collect1Up = content.Load<SoundEffect>("Sounds/smb_1-up");
            SoundBank.Add(EventSounds.Collect1Up, collect1Up);
            SoundEffect brickBump = content.Load<SoundEffect>("Sounds/smb_bump");
            SoundBank.Add(EventSounds.BrickBump, brickBump);
            SoundEffect brickBreak = content.Load<SoundEffect>("Sounds/smb_breakblock");
            SoundBank.Add(EventSounds.BrickBreak, brickBreak);
            SoundEffect pipeTravel = content.Load<SoundEffect>("Sounds/smb_pipe");
            SoundBank.Add(EventSounds.PipeTravel, pipeTravel);
            SoundEffect gameOver = content.Load<SoundEffect>("Sounds/smb_gameover");
            SoundBank.Add(EventSounds.GameOver, gameOver);
            SoundEffect BooSound = content.Load<SoundEffect>("Sounds/smb_boo");
            SoundBank.Add(EventSounds.BooSound, BooSound);
        }

        public void PlaySound(EventSounds eventSound)
        {
            SoundEffectInstance soundEffectInstance = SoundBank[eventSound].CreateInstance();
            soundEffectInstance.IsLooped = false;
            soundEffectInstance.Volume = _volume;
            if (eventSound == EventSounds.Die || eventSound == EventSounds.PipeTravel || eventSound == EventSounds.GameOver)
            {
                MediaPlayer.Pause();
            }
            soundEffectInstance.Play();
        }

        public void Mute(bool mute)
        {
            if(mute)//mute is true then volume = 0.0f
                _volume = 0.0f;
            else
                _volume = 1.0f;
        }


    }
    }



using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.Mario;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;



namespace MarioGame.Entities
{
    public class PoleEntity : Entity
    {
        public PoleEntity(Vector2 position)
        {
            Sprite = new PoleSprite(position, false, 1);
            EntityCollision = new PoleCollison(this);
        }

        public override void CollisionResponse(ICollision collided)
        {
            if (collided is MarioCollision)
            {
                Mario = collided.CurrentEntity as MarioEntity;
                Sprite = new PoleSprite(SpritePosition, true, 5);
                Score(Mario);
            }

        }

        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphics)
        {
            WonGame();
            base.Update(gametime, velocity, graphics);
        }

        public void WonGame()
        {
            if ((Sprite as PoleSprite).stopAnimating && Mario != null)
            {
                Mario.WonGame = true;
            }
        }
        public void Score(MarioEntity mario)
        {
            int jumpHeight = BoundBox.Y - (int)mario.SpritePosition.Y;
            if (jumpHeight<18)
            {
                mario.MarioScore += 100;
                
            }
            if (jumpHeight < 18)
            {
                mario.MarioScore += 100;

            }
            else if (jumpHeight >= 18 && jumpHeight < 58)
            {
                mario.MarioScore += 400;

            }
            else if (jumpHeight >= 58 && jumpHeight < 82)
            {
                mario.MarioScore += 800;

            }
            else if (jumpHeight >= 82 && jumpHeight < 128)
            {
                mario.MarioScore += 2000;

            }
            else if (jumpHeight >= 128 && jumpHeight < 153)
            {
                mario.MarioScore += 5000;

            }


        }



    }
}

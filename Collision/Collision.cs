using System.Collections.Generic;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.Collision
{
    public abstract class Collision : ICollision
    {
        public float FirstContact { get; set; }
        private List<Entity> CollidedList;
        private Entity min;
        private Entity two;
        private Entity third;
        public Entity CurrentEntity { get; set; }
        private const float Delay = 200f;//show for 2 seconds
        private int _elapsed = 0;
        public Sprite CurrentSprite
        {
            get { return CurrentEntity.Sprite; }
            set { CurrentEntity.Sprite = value; }
        }

        public Vector2 FutureLocation
        {
            get { return CurrentEntity.SpritePosition + CurrentEntity.SpriteVelocity; }
        }
        public Rectangle FutureBox
        {
            get { return CurrentSprite.FutureBox(FutureLocation);}
        }
        public Rectangle Intersection { get; set; }
        protected Collision(Entity entity)
        {
            CurrentEntity = entity;
            CollidedList = new List<Entity>();
        }
        public virtual void Detection(GameTime gameTime, ICollision collideObject) //detect IF it will collide.
        {
            if (FutureBox.Intersects(collideObject.CurrentEntity.BoundBox)&&!collideObject.CurrentEntity.Dead)
            {
                third = two;
                two = min;
                collideObject.FirstContact = gameTime.ElapsedGameTime.Milliseconds;
                min = collideObject.CurrentEntity;
                CollidedList.Add(min);
                Intersection = Rectangle.Intersect(FutureBox, collideObject.CurrentSprite.BoundBox);
            }
           
            _elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (_elapsed >= Delay)//To show a black color for collision response.
            {
                CurrentSprite.CollisionResponse(false);
                collideObject.CurrentSprite.CollisionResponse(false);
                _elapsed = 0;
            }
        }

        public void AfterAllDetection()
        {
            if (CollidedList.Count > 0)
            {
                foreach (Entity one in CollidedList)
                {
                    Intersection = Rectangle.Intersect(FutureBox, one.BoundBox);
                    if (one.EntityCollision.FirstContact < min.EntityCollision.FirstContact)
                    {
                        third = two;
                        two = min;
                        min = one;
                    }
                    one.EntityCollision.Response(this);
                    CurrentSprite.CollisionResponse(true);//tint sprite
                    one.Sprite.CollisionResponse(true);//tint sprite
                }
                Intersection = Rectangle.Intersect(FutureBox, min.BoundBox);
                Response(min.EntityCollision);
                if (two != null && FutureBox.Intersects(two.BoundBox) && two!=min)
                {
                    Intersection = Rectangle.Intersect(FutureBox, two.BoundBox);
                    Response(two.EntityCollision);
                }
                if (third != null && FutureBox.Intersects(third.BoundBox) && third!=two&&third!=min)
                {
                    Intersection = Rectangle.Intersect(FutureBox, third.BoundBox);
                    Response(third.EntityCollision);
                }
                CollidedList.Clear();
            }
        }
        public virtual bool MarioState() { return false; }

        public virtual void Response(ICollision collided){}//base response is to do nothing.

        //when currentSprite hit the top of collided object
        public bool TopCollision(ICollision collided) //currentSprite on top.
        {
            return Intersection.Bottom < collided.FutureBox.Bottom && Intersection.Top > FutureBox.Top
                   &&CurrentEntity.SpriteVelocity.Y>0;
        }

        //when currentSprite hit the bottom of collided object
        public bool BottomCollision(ICollision collided)
        {
            return Intersection.Top > collided.FutureBox.Top && Intersection.Bottom <= FutureBox.Bottom&&CurrentEntity.SpriteVelocity.Y<0;
        }

        public bool SideCollision(ICollision collided) //current sprite moving L or R.
        {
            return Intersection.Width < Intersection.Height && (int)CurrentSprite.Velocity.X!=0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Entities
{
    public abstract class Entity:ISprite
    {
        public Sprite Sprite { get; set; }
        public Boolean Dead { get; set; }
        public EventSoundEffects Sounds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<Tile>CollideList { get; set; }//the list of collidable objects
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<Entity> MovingEntities { get; set; }//list of moving collidable objects
        public MarioEntity Mario { get; set; }

        public Vector2 SpritePosition
        {
            get { return Sprite.Position;} 
            set { Sprite.Position = value; }
        }
        public Vector2 SpriteVelocity
        {
            get { return Sprite.Velocity; }
            set { Sprite.Velocity = value; }
        }
        public ICollision EntityCollision { get; set; }
        public Rectangle BoundBox
        {
            get { return Sprite.BoundBox; }
        }
        public virtual void AddItem(Entity item, List<Tile> tiles,List<Entity> moving, MarioEntity mario){}//only add one for now

        public virtual void Reveal(MarioEntity mario) {
        }
        public virtual void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphics)
        {
            Sprite.Update(gametime,SpriteVelocity,graphics);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        public virtual void CollisionResponse(ICollision collided) { }
    }
}

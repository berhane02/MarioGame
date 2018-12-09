using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Entities;
using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface ICollision
    {
        float FirstContact { get; set; }
        Entity CurrentEntity { get; set; }
        Sprite CurrentSprite { get; set; }
        Rectangle Intersection { get; set; }
        Rectangle FutureBox { get; }
        void Detection(GameTime gametime, ICollision collideObject);
        void AfterAllDetection();
        void Response(ICollision collidedObject);
        bool TopCollision(ICollision collided);
        bool BottomCollision(ICollision collided);
        bool SideCollision(ICollision collided);
        bool MarioState();

    }
}

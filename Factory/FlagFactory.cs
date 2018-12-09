using MarioGame.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Factory
{
    public static class FlagFactory
    {
        public static Entity BuildSprite(Vector2 loc)
        {
            return new PoleEntity(loc);
        }
    }
}
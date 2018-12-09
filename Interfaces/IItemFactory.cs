using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Entities;
using MarioGame.Factory;
using MarioGame.Levels;
using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface IItemFactory
    {
         Entity BuildItemEntity(ItemFactory.ItemType type, Vector2 loc, EventSoundEffects sounds);
    }
}

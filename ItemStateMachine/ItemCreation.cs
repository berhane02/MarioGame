using MarioGame.Entities;
using MarioGame.Levels;
using Microsoft.Xna.Framework;

namespace MarioGame
{
    static class ItemCreation
    {
        public enum ItemType
        {
            Flower, Coin,
            RedMushroom, GreenMushroom, Star, Axe
        }

        public static Entity BuildItemEntity(ItemType type, Vector2 loc, EventSoundEffects sounds)
        {
            Entity toReturn = null;

            switch (type)
            {
                case ItemType.Flower:
                    toReturn = new FlowerEntity(loc, sounds);
                    break;
                case ItemType.Coin:
                    toReturn = new CoinEntity(loc, sounds);
                    break;
                case ItemType.RedMushroom:
                    toReturn = new RedMushroomEntity(loc, sounds);
                    break;
                case ItemType.GreenMushroom:
                    toReturn = new GreenMushroomEntity(loc, sounds);
                    break;
                case ItemType.Star:
                    toReturn = new StarEntity(loc, sounds);
                    break;
                case ItemType.Axe:
                    toReturn = new AxeEntity(loc, sounds);
                    break;

            }
            return toReturn;
        }
    }
}


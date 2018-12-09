using Microsoft.Xna.Framework;
using MarioGame;
using System;
using MarioGame.Entities;
using MarioGame.Mario;
using MarioGame.Sprites;

namespace MarioGame.Factory
{
    public static class EnemyFactory
    {

        public enum EnemyType
        {
            WalkingGoomba,
            GreenKoopa, RedKoopa, Piranha, Bowser
        }
        
        public static Entity BuildEnemySprite(EnemyType type, Vector2 loc, MarioEntity mario)
        {
            Entity toReturn = null;
            
            switch (type)
            {
                case EnemyType.WalkingGoomba:
                    toReturn = new GoombaEntity(loc);
                    break;
                case EnemyType.GreenKoopa:
                    toReturn = new GreenKoopaEntity(loc);
                    break;
                case EnemyType.RedKoopa:
                    toReturn = new RedKoopaEntity(loc);
                    break;
                case EnemyType.Piranha:
                    toReturn = new PiranhaEntity(loc);
                    break;
                case EnemyType.Bowser:
                    toReturn = new BowserEntity(loc, mario);
                    break;
            }
            return toReturn;
        }
    }
}

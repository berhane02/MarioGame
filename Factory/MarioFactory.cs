using MarioGame.Interfaces;
using MarioGame.Mario;
using MarioGame.Mario.MarioActionState;
using MarioGame.Mario.MarioPowerUp;
using MarioGame.MarioActionState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MarioGame.Factory
{
    public class MarioFactory : IMarioFactory
    {
        public enum Mario
        {

            //standard mario
            Dead = 0x0,
            LeftCrouchingSm = 0x010,
            LeftIdlingSm = 0x030,
            LeftJumpingSm = 0x040,
            LeftRunningSm = 0x050,
            RightCrouchingSm = 0x060,
            RightIdlingSm = 0x080,
            RightJumpingSm = 0x090,
            RightRunningSm = 0x0A0,

            //super mario
            LeftCrouchingBm = 0x0100,
            LeftIdlingBm = 0x0300,
            LeftJumpingBm = 0x0400,
            LeftRunningBm = 0x0500,
            RightCrouchingBm = 0x0600,
            RightIdlingBm = 0x0800,
            RightJumpingBm = 0x0900,
            RightRunningBm = 0x0A00,

            //fire mario
            LeftCrouchingFm = 0x01000,
            LeftIdlingFm = 0x03000,
            LeftJumpingFm = 0x04000,
            LeftRunningFm = 0x05000,
            RightCrouchingFm = 0x06000,
            RightIdlingFm = 0x08000,
            RightJumpingFm = 0x09000,
            RightRunningFm = 0x0A000,

            //Invicble mario
            LeftCrouchingIm = 0x001,
            LeftIdlingIm = 0x003,
            LeftJumpingIm = 0x004,
            LeftRunningIm = 0x005,
            RightCrouchingIm = 0x006,
            RightIdlingIm = 0x008,
            RightJumpingIm = 0x009,
            RightRunningIm = 0x00A,


        }

        public Sprite MarioSprite(IMarioPowerUpState power, IMarioActionState action, bool Facing, Vector2 loc)
        {
            int type = MarioSpriteType(action, Facing);
            type = MarioPowerUp(power, type);
            return BuildSprite((Mario)type, loc);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public int MarioPowerUp(IMarioPowerUpState p, int m)//bit shifting to correct combination of 2 states
        {
            switch (p)
            {
                case MarioStandardState _:
                    break;
                case MarioSuperState _:
                    m = m << 4;
                    break;
                case MarioFireState _:
                    m = m << 8;
                    break;
                case MarioDeadState _:
                    m = (int)Mario.Dead;
                    break;
                case MarioInvincibleState _:
                    m = m >> 4;
                    break;
            }
            return m;
        }
        public static int MarioSpriteType(IMarioActionState a, bool Facing)//bit adding to find correct action state enum.
        {
            int buildMario = 0x010; //default to crouching
            switch (a)
            {
                case Falling _:
                    buildMario += 0x030;
                    break;
                case Idling _:
                    buildMario += 0x020;
                    break;
                case Jumping _:
                    buildMario += 0x030;
                    break;
                case Running _:
                    buildMario += 0x040;
                    break;
            }

            if (Facing)//right
            {
                buildMario += 0x050;
            }
            return buildMario;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public Sprite BuildSprite(Mario sprite, Vector2 location) //finding the Sprite corresponding to the enum.
        {
            Sprite toReturn = null;
            switch (sprite)
            {
                case Mario.Dead:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/DeadMario");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftIdlingSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/LeftIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftIdlingFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/LeftIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftIdlingBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/LeftIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftIdlingIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/LIdle");
                        toReturn = new MarioSprite(product, true, 1, 2, location);
                        break;
                    }
                case Mario.RightIdlingSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/RightIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightIdlingFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/RightIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightIdlingBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/RightIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightIdlingIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/RIdle");
                        toReturn = new MarioSprite(product, true, 1, 2, location);
                        break;
                    }
                case Mario.LeftCrouchingSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/LeftIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftCrouchingBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/LeftCrouching");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftCrouchingFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/LeftCrouching");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftCrouchingIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/LCrouch");
                        toReturn = new MarioSprite(product, true, 1, 2, location);
                        break;
                    }
                case Mario.RightCrouchingSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/RightIdle");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightCrouchingFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/RightCrouching");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightCrouchingBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/RightCrouching");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightCrouchingIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/RCrouch");
                        toReturn = new MarioSprite(product, true, 1, 2, location);
                        break;
                    }
                case Mario.LeftRunningSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/LeftMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.LeftRunningFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/LeftMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.LeftRunningBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/LeftMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.LeftRunningIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/LMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.RightRunningSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/RightMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.RightRunningFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/RightMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.RightRunningBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/RightMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.RightRunningIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/RMoving");
                        toReturn = new MarioSprite(product, true, 1, 4, location);
                        break;
                    }
                case Mario.LeftJumpingSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/LeftJumping");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftJumpingFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/LeftJumping");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftJumpingBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/LeftJumping");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.LeftJumpingIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/LJump");
                        toReturn = new MarioSprite(product, true, 1, 2, location);
                        break;
                    }
                case Mario.RightJumpingSm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("StandardMario/RightJumping");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightJumpingFm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("FireMario/RightJumping");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightJumpingBm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("SuperMario/RightJumping");
                        toReturn = new MarioSprite(product, false, 1, 1, location);
                        break;
                    }
                case Mario.RightJumpingIm:
                    {
                        Texture2D product = Game1.ContentLoad.Load<Texture2D>("InvincibleMario/RJump");
                        toReturn = new MarioSprite(product, true, 1, 2, location);
                        break;
                    }
            }
            return toReturn;
        }
    }
}

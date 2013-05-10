///////////////////////////////////////////////////////////
//  PlayerEnviromentDetect.cs
//  Implementation of the Class PlayerEnviromentDetect
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:50:50
//  Original author: Filip
///////////////////////////////////////////////////////////


using System;
using Sprites;
using System.Collections.Generic;
using Collision;
namespace Collision
{
    public class PlayerEnviromentDetect : ICollisionDetect
    {

        public PlayerEnviromentDetect()
        {
        }

        /// 
        /// <param name="mSpriteCollection"></param>
        /// <param name="mSprite2"></param>
        /// <param name="mSprite1"></param>
        public void Detect(ISpriteContainerCollision mSpriteCollection, ISprite player, ISprite environment)
        {

            float margin = player.Velocity.X + 10;

            //Left side
            if (player.MyRectangle.X + player.MyRectangle.Width >= environment.MyRectangle.X &&
                (player.MyRectangle.X + player.MyRectangle.Width) <= (environment.MyRectangle.X + margin) //&&
                //player.Velocity.X > 0
                )
            {
                new PlayerEnvironmentLeftEvent(player, environment);
            }

            //Right side
            else if (player.MyRectangle.X >= (environment.MyRectangle.X + environment.MyRectangle.Width - margin) &&
                     player.MyRectangle.X <= (environment.MyRectangle.X + environment.MyRectangle.Width) //&&
                     //player.Velocity.X < 0
                )
            {
                new PlayerEnvironmentRightEvent(player, environment);
            }

            //Top side
            else if (player.MyRectangle.Y + player.MyRectangle.Height >= environment.MyRectangle.Y &&
                     player.MyRectangle.Y + player.MyRectangle.Height <
                     environment.MyRectangle.Y + (environment.MyRectangle.Height / 2) ) 
            {
                new PlayerEnvironmentTopEvent(player, environment);
            }
            //Bottom side
            else if (player.MyRectangle.Y <= environment.MyRectangle.Y + environment.MyRectangle.Height &&
                     player.MyRectangle.Y > environment.MyRectangle.Y + (environment.MyRectangle.Height / 2) )
            {
                new PlayerEnvironmentBottomEvent(player, environment);
            }
        }

    }//end PlayerEnviromentDetect

}//end namespace CollisionControl
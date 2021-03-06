///////////////////////////////////////////////////////////
//  CollisionControl.cs
//  Implementation of the Class CollisionControl
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:50:49
//  Original author: Filip V
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using GameState;
using Sprites;


namespace Collision
{
    public class CollisionControl : ICollisionControl
    {
        private ISpriteContainerCollision _spriteContainerCollision;
        private ICollisionDetect _playerEnviromentDetect;
        private ICollisionDetect _playerMonsterDetect;
        private ICollisionDetect _monsterEnviromentDetect;
        private ICollisionDetect _playerFinishlineDetect;
        private ICollisionDetect _playerCDDetect;

        public CollisionControl(ICollisionDetect mPlayerEnviromentDetect, ICollisionDetect mPlayerMonsterDetect,
                                ICollisionDetect mMonsterEnvironmentDetect, ICollisionDetect mPlayerFinishlineDetect,
                                ICollisionDetect playerCDDetect)
        {
            
            _playerEnviromentDetect = mPlayerEnviromentDetect;
            _playerMonsterDetect = mPlayerMonsterDetect;
            _monsterEnviromentDetect = mMonsterEnvironmentDetect;
            _playerFinishlineDetect = mPlayerFinishlineDetect;
            _playerCDDetect = playerCDDetect;
        }
        

        /// 
        /// <param name="mSprites"></param>
        public void Update(ISpriteContainerCollision mSpriteContainerCollision)
        {
            _spriteContainerCollision = mSpriteContainerCollision;
            CheckForPlayerCollision();
            CheckForMonsterCollision();
            CheckForFinishCollision();
        }

        private void CheckForPlayerCollision()
        {
            for (int i = 0; i < _spriteContainerCollision.SpriteList[(int) listTypes.Player].Count; i++)
            {
                /* Checking with Monster */
                for (int j = 0; j < _spriteContainerCollision.SpriteList[(int) listTypes.Monster].Count; j++)
                {
                    if (_spriteContainerCollision.SpriteList[(int) listTypes.Player][i].MyRectangle.Intersects(
                        _spriteContainerCollision.SpriteList[(int) listTypes.Monster][j].MyRectangle))
                    {
                        _playerMonsterDetect.Detect(_spriteContainerCollision,
                                                    _spriteContainerCollision.SpriteList[(int) listTypes.Player][i],
                                                    _spriteContainerCollision.SpriteList[(int) listTypes.Monster][j]);
                    }
                }

                /* Checking with Evironment */
                for (int j = 0; j < _spriteContainerCollision.SpriteList[(int) listTypes.Environment].Count; j++)
                {
                    if (_spriteContainerCollision.SpriteList[(int) listTypes.Player][i].MyRectangle.Intersects(
                        _spriteContainerCollision.SpriteList[(int) listTypes.Environment][j].MyRectangle))
                    {
                        _playerEnviromentDetect.Detect(_spriteContainerCollision,
                                                       _spriteContainerCollision.SpriteList[(int) listTypes.Player][i],
                                                       _spriteContainerCollision.SpriteList[(int) listTypes.Environment][j]);
                    }
                }


                /* Checking with CD */
                for (int j = 0; j < _spriteContainerCollision.SpriteList[(int)listTypes.CD].Count; j++)
                {
                    if (_spriteContainerCollision.SpriteList[(int)listTypes.Player][i].MyRectangle.Intersects(
                        _spriteContainerCollision.SpriteList[(int)listTypes.CD][j].MyRectangle))
                    {
                        _playerCDDetect.Detect(_spriteContainerCollision,
                                                       _spriteContainerCollision.SpriteList[(int)listTypes.Player][i],
                                                       _spriteContainerCollision.SpriteList[(int)listTypes.CD][j]);
                    }
                }
            }
        }


        private void CheckForMonsterCollision()
        {
            for (int i = 0; i < _spriteContainerCollision.SpriteList[(int) listTypes.Monster].Count; i++)
            {
                /* Checking with Evironment */
                for (int j = 0; j < _spriteContainerCollision.SpriteList[(int) listTypes.Environment].Count; j++)
                {
                    if (_spriteContainerCollision.SpriteList[(int) listTypes.Monster][i].MyRectangle.Intersects(
                        _spriteContainerCollision.SpriteList[(int) listTypes.Environment][j].MyRectangle))
                    {
                        _monsterEnviromentDetect.Detect(_spriteContainerCollision,
                                                        _spriteContainerCollision.SpriteList[(int) listTypes.Monster][i],
                                                        _spriteContainerCollision.SpriteList[(int) listTypes.Environment][j]);
                    }
                }
            }
        }

        private void CheckForFinishCollision()
        {
            for (int i = 0; i < _spriteContainerCollision.SpriteList[(int)listTypes.Player].Count; i++)
            {
                /* Checking with Evironment */
                for (int j = 0; j < _spriteContainerCollision.SpriteList[(int)listTypes.FinishLine].Count; j++)
                {
                    if (_spriteContainerCollision.SpriteList[(int)listTypes.Player][i].MyRectangle.Intersects(
                        _spriteContainerCollision.SpriteList[(int)listTypes.FinishLine][j].MyRectangle))
                    {
                        _playerFinishlineDetect.Detect( _spriteContainerCollision, 
                                                        _spriteContainerCollision.SpriteList[(int)listTypes.Player][i],
                                                        _spriteContainerCollision.SpriteList[(int)listTypes.FinishLine][j] );
                    }
                }
            }
            
        }
    }

    //end CollisionControl
}

//end namespace CollitionControl
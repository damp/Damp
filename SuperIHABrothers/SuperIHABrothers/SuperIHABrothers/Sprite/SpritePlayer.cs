///////////////////////////////////////////////////////////
//  SpritePlayer.cs
//  Implementation of the Class SpritePlayer
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:48:22
//  Original author: Space-Punk
///////////////////////////////////////////////////////////


using System;
using Microsoft.Xna.Framework;



namespace Sprites {
	public class SpritePlayer : ISprite, ISpriteAnchor {

	    public int Speed { get; set; }
	    public Vector2 MyPosition { get; set; }
        public Rectangle MyRectangle { get; set; }
        public Vector2 Velocety { get; set; }

		public SpritePlayer(){
            throw new NotImplementedException();
		}



		public void Draw(){
            throw new NotImplementedException();
		}




	    public void Update(){
            throw new NotImplementedException();
		}

	}//end SpritePlayer

}//end namespace Sprites
///////////////////////////////////////////////////////////
//  Anchor.cs
//  Implementation of the Class Anchor
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:48:21
//  Original author: Space-Punk
///////////////////////////////////////////////////////////





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameState;


namespace Sprites {
	public class Anchor : IAnchor, IAnchorUpdate {

	    private Vector2 _velocety;
        public Vector2 Velocety { get { return _velocety; } set { _velocety = value; } }
	    private IKeybordInput _KeyInput;
        public Vector2 Position { get; set; }
        public int Speed { get; set; }

        public Anchor(IKeybordInput mKey)
        {
            Position = new Vector2(0,0);
            _velocety = new Vector2(0,0);
		    _KeyInput = mKey;
            Speed = -3;
        }

		public void Update(GameTime time)
		{
            _velocety.X = 0;
            if (_KeyInput.IsRightPressed)
                _velocety.X += Speed;
            if (_KeyInput.IsLeftPressed)
                _velocety.X += -Speed;
               
		    Position += _velocety;
		}


	    
	}//end Anchor

}//end namespace Sprites
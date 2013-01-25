using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FredLib.Input;
using Microsoft.Xna.Framework.Input;

namespace GameJam2013.Objects
{
    class MovingPlatform : Wall
    {
        public MovingPlatform(Texture2D image, Vector2 pos, int width, int height)
            : base(image, pos, width, height) {}

        public void move(int amount)
        {
            pos.Y -= amount;
            rect.Y = (int) pos.Y;
        }

        public override void HandleInput(Input input)
        {
            if (input.IsKeyDown(Keys.Q))
            {
                move(8);
            }
        }
    }
}

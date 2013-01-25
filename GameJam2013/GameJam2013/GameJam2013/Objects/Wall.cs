using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DigestionDuel.Utilities;

namespace DigestionDuel.Objects
{
    class Wall : Collideable
    {
        SpriteSheet spriteSheet;

        public Wall(Vector2 pos, int width, int height)
            : base(pos, width, height) { }

        public Wall(Texture2D image ,Vector2 pos, int width, int height)
            : base(pos,width,height)
        {
            spriteSheet = new SpriteSheet(image, width, height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            spriteSheet.Draw(sBatch, rect);
        }
        
        public void SetStaticPos(Point pos)
        {
            spriteSheet.SetStaticPos(pos);
        }

        public override void onCollHit(Collideable c, float x, float y)
        {
            if (y != 0)
            {
                if (y > 0) //DOWN
                {
                    c.rect.Y = rect.Y - c.rect.Height;
                }
                else //UP
                {
                    c.rect.Y = rect.Y + rect.Height;
                }
            }
            if (x != 0)
            {
                if (x > 0) //RIGHT
                {
                    c.rect.X = rect.X - c.rect.Width;
                }
                else //LEFT
                {
                    c.rect.X = rect.X + rect.Width;
                }
            }
            c.pos.X = c.rect.X;
            c.pos.Y = c.rect.Y;
        }
    }
}

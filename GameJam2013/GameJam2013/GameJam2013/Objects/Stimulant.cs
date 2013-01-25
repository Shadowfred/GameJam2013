using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FredLib.Graphics;

namespace GameJam2013.Objects
{
    class Stimulant : Collideable
    {
        public static Texture2D attackImage;
        SpriteSheet spriteSheet;
        public bool isHit = false;
        bool flipped = false;
        Color tint;

        public Stimulant() { }

        public Stimulant(Texture2D image, Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
            spriteSheet = new SpriteSheet(image, width, height);
            List<Point> animation = new List<Point>();
            //Idle
            animation.Add(new Point(4, 2));
            animation.Add(new Point(5, 2));
            animation.Add(new Point(6, 2));
            animation.Add(new Point(7, 2));
            spriteSheet.AddNewAnimation("idle", animation);
            spriteSheet.SetAnim("idle", 0.6f, flipped);
            tint = Color.White;
            spriteSheet.SetTint(tint);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            spriteSheet.Update(gameTime);
            if (!isHit)
            {
                spriteSheet.Draw(sBatch, rect);
            }
        }

        public override void onCollHitting(Collideable c, float x, float y)
        {
            if (c is Player)
            { 
            }
        }

        public void SetColour(Color colour)
        {
            tint = colour;
            spriteSheet.SetTint(colour);
        }

        public void Hit()
        {
            isHit = true;
        }
    }
}

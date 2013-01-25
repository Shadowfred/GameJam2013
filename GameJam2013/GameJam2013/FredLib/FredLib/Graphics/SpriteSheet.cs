using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FredLib.Graphics
{
    public class SpriteSheet
    {
        Texture2D image;
        Rectangle rect;
        Dictionary<string, List<Point>> animations;
        int width;
        int height;
        int frame = 0;
        int maxFrames = 0;
        public bool flipped = false;
        bool looped = false;
        bool ended = false;

        string currentAnim = "";
        public string CurrentAnim
        {
            get { return currentAnim; }
        }
        float animLength = 0.0f;
        float animTime = 0.0f;
        Color tint = Color.White;

        public SpriteSheet(Texture2D image, int width, int height)
        {
            this.image = image;
            this.width = width;
            this.height = height;
            rect = new Rectangle(0, 0, width, height);
            animations = new Dictionary<string, List<Point>>();
        }

        public void AddNewAnimation(string name, List<Point> positions)
        {
            animations.Add(name, positions);
        }

        public void SetAnim(string name)
        {
            currentAnim = name;
            frame = 0;
            maxFrames = animations[name].Count;
            animLength = 0.0f;
            animTime = 0.0f;
            rect.X = animations[currentAnim][frame].X * width;
            rect.Y = animations[currentAnim][frame].Y * height;
            flipped = false;
        }

        public void SetAnim(string name, bool flip)
        {
            currentAnim = name;
            frame = 0;
            maxFrames = animations[name].Count;
            animLength = 0.0f;
            animTime = 0.0f;
            rect.X = animations[currentAnim][frame].X * width;
            rect.Y = animations[currentAnim][frame].Y * height;
            flipped = flip;
        }

        public void SetAnim(string name, float duration)
        {
            currentAnim = name;
            frame = 0;
            maxFrames = animations[name].Count;
            animLength = duration;
            animTime = 0.0f;
            rect.X = animations[currentAnim][frame].X * width;
            rect.Y = animations[currentAnim][frame].Y * height;
            flipped = false;
        }

        public void SetAnim(string name, float duration, bool flip)
        {
            currentAnim = name;
            frame = 0;
            maxFrames = animations[name].Count;
            animLength = duration;
            animTime = 0.0f;
            rect.X = animations[currentAnim][frame].X * width;
            rect.Y = animations[currentAnim][frame].Y * height;
            flipped = flip;
        }

        public void Update(GameTime gameTime)
        {
            if (currentAnim != "" && animLength != 0.0f)
            {
                animTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animTime > animLength)
                {
                    animTime -= animLength;
                }
                frame = (int)(maxFrames * (animTime / animLength));
                rect.X = animations[currentAnim][frame].X * width;
                rect.Y = animations[currentAnim][frame].Y * height;
            }
        }

        public void Draw(SpriteBatch sBatch, Rectangle posRect)
        {
            if (!flipped)
            {
                sBatch.Draw(image, posRect, rect, tint, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
            else
            {
                sBatch.Draw(image, posRect, rect, tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
        }

        public void Draw(SpriteBatch sBatch, Rectangle posRect, float rotation)
        {
            if (!flipped)
            {
                sBatch.Draw(image, posRect, rect, tint, rotation, Vector2.Zero, SpriteEffects.None, 0);
            }
            else
            {
                sBatch.Draw(image, posRect, rect, tint, rotation, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
        }

        public void SetTint(Color colour)
        {
            tint = colour;
        }

        public void SetStaticPos(Point pos)
        {
            rect.X = pos.X * width;
            rect.Y = pos.Y * height;
        }

        void GetAnimFromFile(string fName)
        {
            //input.ReadLine()
            StreamReader input = new StreamReader(fName);
            

            input.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FredLib.UI.Elements
{
    /// <summary>
    /// An element that will display an image.
    /// </summary>
    public class UIPicture : UIElement
    {
        #region Attributes
        Texture2D image;

        //Rectangle rect;
        #endregion

        #region Initialisation
        /// <summary>
        /// Creates a new picture.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="pos"></param>
        public UIPicture(Texture2D i, Vector2 pos)
            : base()
        {
            image = i;
            position = pos;
            relativePosition = pos;
            rect = image.Bounds;
        }

        /// <summary>
        /// Creates a new picture.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="pos"></param>
        public UIPicture(UIElement p, Texture2D i, Vector2 pos)
            : base(p)
        {
            image = i;
            relativePosition = pos;
            position = parent.Position + pos;
            rect = new Rectangle((int)(position.X), (int)(position.Y), image.Width, image.Height);
        }

        public UIPicture(UIElement p, Texture2D i, Vector2 pos, Vector2 size)
            : base(p)
        {
            image = i;
            relativePosition = pos;
            position = parent.Position + pos;
            rect = new Rectangle((int)(position.X), (int)(position.Y), (int)(size.X), (int)(size.Y));
        }    

        #endregion

        #region Update, Handle Input, Draw

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="input">The current input state.</param>
        public override void HandleInput(Input.Input input)
        {
            if (active)
            {
                if (rect.Contains(input.MouseX(), input.MouseY()))
                {
                    HandleInputChildren(input);
                }
            }
        }

        /// <summary>
        /// Draws the picture.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun.</param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (active)
            {
                sBatch.Draw(image, rect, tint * alpha);
                DrawChildren(gameTime, sBatch);
            }
        }

        public override void RescaleElement(int width, int height)
        {
            base.RescaleElement(width, height);
            rect.Width = width;
            rect.Height = height;
        }
        #endregion
    }
}

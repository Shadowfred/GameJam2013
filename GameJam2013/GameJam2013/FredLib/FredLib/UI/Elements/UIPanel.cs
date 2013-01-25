using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FredLib.Input;

namespace FredLib.UI.Elements
{
    /// <summary>
    /// An element that will mostly contain other elements.
    /// </summary>
    public class UIPanel : UIElement
    {
        #region Attributes
        Texture2D image;
        /// <summary>
        /// The image that will be drawn.
        /// </summary>
        public Texture2D Image
        {
            get {return image;}
            set {image = value;}
        }

        protected bool free;
        /// <summary>
        /// True if the panel will accept input no matter where the mouse is.
        /// </summary>
        public bool Free
        {
            get { return free; }
            set { free = value; }
        }

        protected bool drawable;
        /// <summary>
        /// True if the panel will draw.
        /// </summary>
        public bool Drawable
        {
            get { return drawable; }
            set { drawable = value; }
        }

        public Texture2D corner;
        public Texture2D border;

        //int width;
        public int Width
        {
            get {return width;}
            set {width = value;}
        }

        //int height;
        public int Height
        {
            get {return height;}
            set {height = value;}
        }

        public bool drawBorder = false;

        #endregion

        #region Initialisation
        /// <summary>
        /// Creates a new UIPanel
        /// </summary>
        /// <param name="i">The image to draw.</param>
        /// <param name="pos">The position of the panel.</param>
        /// <param name="w">The width of the panel.</param>
        /// <param name="h">The height of the panel</param>
        public UIPanel(Texture2D i, Vector2 pos, int w, int h)
            : base()
        {
            image = i;
            relativePosition = pos;
            position = pos;
            width = w;
            height = h;
            rect = new Rectangle((int)pos.X, (int)pos.Y, w, h);
            drawable = true;
            free = false;
        }

        /// <summary>
        /// Creates a new UIPanel
        /// </summary>
        /// <param name="p">The parent of the panel.</param>
        /// <param name="i">The image to draw.</param>
        /// <param name="pos">The relative position of the panel.</param>
        /// <param name="w">The width of the panel.</param>
        /// <param name="h">The height of the panel</param>
        public UIPanel(UIElement p, Texture2D i, Vector2 pos, int w, int h)
            : base(p)
        {
            image = i;
            relativePosition = pos;
            position = parent.Position + pos;
            width = w;
            height = h;
            rect = new Rectangle((int)(position.X), (int)(position.Y), w, h);
            drawable = true;
            free = false;
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
                if (free || rect.Contains(input.MouseX(), input.MouseY()))
                {
                    HandleInputChildren(input);
                }
            }
        }

        /// <summary>
        /// Draws the panel.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun.</param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (active)
            {
                if (drawable)
                {
                    sBatch.Draw(image, position, new Rectangle(0, 0, width, height), tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    if (drawBorder)
                    {
                        DrawBorder(sBatch);
                    }
                }

                DrawChildren(gameTime, sBatch);
            }
        }
        #endregion

        #region Public Functions
        public override void UpdatePosition()
        {
            base.UpdatePosition();
            rect.Width = width;
            rect.Height = height;
        }
        #endregion

        #region Private Functions
        void DrawBorder(SpriteBatch sBatch)
        {
            //LONG THINGS
            //TOP
            sBatch.Draw(border,
                position,
                new Rectangle(0, 0, width, border.Height), tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            //BOTTOM
            sBatch.Draw(border,
                new Vector2(position.X, position.Y + height - border.Height),
                new Rectangle(0, 0, width, border.Height), tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
            //LEFT
            sBatch.Draw(border,
                new Vector2(position.X + border.Height, position.Y),
                new Rectangle(0, 0, height, border.Height), tint * alpha, MathHelper.ToRadians(90), Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
            //RIGHT
            sBatch.Draw(border,
                new Vector2(position.X + width, position.Y),
                new Rectangle(0, 0, height, border.Height), tint * alpha, MathHelper.ToRadians(90), Vector2.Zero, 1, SpriteEffects.None, 0);
            //CORNERS
            //TOPLEFT
            sBatch.Draw(corner,
                position,
                corner.Bounds, tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            //TOPRIGHT
            sBatch.Draw(corner,
                new Vector2(position.X, position.Y + height - corner.Height),
                corner.Bounds, tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
            //BOTTOMLEFT
            sBatch.Draw(corner,
                new Vector2(position.X + width - corner.Width, position.Y),
                corner.Bounds, tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            //BOTTOMRIGHT
            sBatch.Draw(corner,
                new Vector2(position.X + width, position.Y + height),
                corner.Bounds, tint * alpha, MathHelper.ToRadians(180), Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        #endregion
    }
}

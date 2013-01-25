using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FredLib.Input;

namespace FredLib.UI.Elements
{
    public class UISlider : UIElement
    {
        #region Attributes
        int yOff, xOff;
        bool tabClick;

        Texture2D image;
        /// <summary>
        /// The background of the scroll panel.
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        Texture2D tabImage;
        /// <summary>
        /// The tab on the side.
        /// </summary>
        public Texture2D TabImage
        {
            get { return tabImage; }
            set { tabImage = value; }
        }

        public float scale
        {
            get;
            private set;
        }

        Rectangle tabRect;
        /// <summary>
        /// The rectangle that contains the scroll button.
        /// </summary>
        public Rectangle ScrollRect
        {
            get { return rect; }
            set { rect = value; }
        }
        #endregion

        #region Initialisation
        /// <summary>
        /// Creates a new UIPanel
        /// </summary>
        /// <param name="i">The image to draw.</param>
        /// <param name="pos">The position of the panel.</param>
        /// <param name="w">The width of the panel.</param>
        /// <param name="h">The height of the panel</param>
        public UISlider(Texture2D i, Texture2D tabImage,Vector2 pos, int w, int h)
            : base()
        {
            image = i;
            this.tabImage = tabImage;
            relativePosition = pos;
            position = pos;
            width = w;
            height = h;
            rect = new Rectangle((int)pos.X, (int)pos.Y, w, h);
        }

        /// <summary>
        /// Creates a new UIScrollPanel
        /// </summary>
        /// <param name="p">The parent of the scroll panel.</param>
        /// <param name="i">The image to draw.</param>
        /// <param name="pos">The position of the panel.</param>
        /// <param name="w">The width of the panel.</param>
        /// <param name="h">The height of the panel</param>
        public UISlider(UIElement p, Texture2D i, Texture2D tabImage, Vector2 pos, int w, int h)
            : base(p)
        {
            image = i;
            this.tabImage = tabImage;
            relativePosition = pos;
            position = pos + p.Position;
            width = w;
            height = h;
            rect = new Rectangle((int)position.X, (int)position.Y, w, h);
            tabRect = new Rectangle((int)position.X, (int)position.Y, w / 10, h);
            yOff = 0;
            tabClick = false;
        }

        public UISlider(UIElement p, float scale, Texture2D i, Texture2D tabImage, Vector2 pos, int w, int h)
            : base(p)
        {
            image = i;
            this.tabImage = tabImage;
            relativePosition = pos;
            position = pos + p.Position;
            this.scale = scale;
            width = w;
            height = h;
            rect = new Rectangle((int)position.X, (int)position.Y, w, h);
            tabRect = new Rectangle((int)(position.X+w*0.9f*scale), (int)position.Y, w / 10, h);
            yOff = 0;
            tabClick = false;
        }
        #endregion

        #region Update, Handle Input, Draw
        public override void HandleInput(Input.Input input)
        {
            if (active)
            {
                if (state != UIState.Disabled)
                {
                    InputState leftState = input.GetMouseState(MouseButtons.Left);
                    if (tabRect.Contains(input.MouseX(), input.MouseY()))
                    {
                        if (!tabClick && leftState == InputState.Pressed)
                        {
                            tabClick = true;
                            yOff = input.MouseY() - tabRect.Y;
                            xOff = input.MouseX() - tabRect.X;
                        }
                    }
                    if (tabClick && (leftState == InputState.Held || leftState == InputState.Pressed))
                    {
                        tabRect.X = input.MouseX() - xOff;
                        if (tabRect.X < position.X)
                        {
                            tabRect.X = (int)position.X;
                        }
                        else if (tabRect.X > position.X + width - tabRect.Width)
                        {
                            tabRect.X = (int)(position.X + width - tabRect.Width);
                        }
                        scale = (tabRect.X - rect.X) / (rect.Width * 0.9f);
                        OnMove();
                    }
                    else if (tabClick && (leftState == InputState.Released))
                    {
                        tabClick = false;
                        OnRelease();
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (active)
            {
                Rectangle oldRect = sBatch.GraphicsDevice.ScissorRectangle;

                //Draw background
                sBatch.Draw(image, position, new Rectangle(0, 0, width, height), tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                //Draw tab
                sBatch.Draw(tabImage, tabRect, tint * alpha);

                sBatch.GraphicsDevice.ScissorRectangle = rect;
                DrawChildren(gameTime, sBatch);
                sBatch.GraphicsDevice.ScissorRectangle = oldRect;
            }

        }
        #endregion

        #region Events
        public event EventHandler Moved;

        protected virtual void OnMove()
        {
            if (Moved != null)
            {
                Moved(this, new EventArgs());
            }
        }
        #endregion

        #region Public Functions
        public override void UpdatePosition()
        {
            tabRect.X -= rect.X;
            tabRect.Y -= rect.Y;
            base.UpdatePosition();
            tabRect.X += rect.X;
            tabRect.Y += rect.Y;
        }
        #endregion
    }
}

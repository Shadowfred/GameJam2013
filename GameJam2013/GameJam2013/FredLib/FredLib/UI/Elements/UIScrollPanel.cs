using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FredLib.Input;

namespace FredLib.UI.Elements
{
    /// <summary>
    /// Functions like a panel, but scrolls.
    /// </summary>
    public class UIScrollPanel : UIElement
    {
        #region Attributes
        int yOff, xOff;
        bool scrollClick;

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

        int extraHeight;
        int scrollDist;
        float scale;

        Rectangle scrollRect;
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
        public UIScrollPanel(Texture2D i, Texture2D tabImage,Vector2 pos, int w, int h)
            : base()
        {
            image = i;
            this.tabImage = tabImage;
            relativePosition = pos;
            position = pos;
            width = w;
            height = h;
            rect = new Rectangle((int)pos.X, (int)pos.Y, w, h);
            scrollDist = 0;
        }

        /// <summary>
        /// Creates a new UIScrollPanel
        /// </summary>
        /// <param name="p">The parent of the scroll panel.</param>
        /// <param name="i">The image to draw.</param>
        /// <param name="pos">The position of the panel.</param>
        /// <param name="w">The width of the panel.</param>
        /// <param name="h">The height of the panel</param>
        public UIScrollPanel(UIElement p, Texture2D i, Texture2D tabImage, Vector2 pos, int w, int h)
            : base(p)
        {
            image = i;
            this.tabImage = tabImage;
            relativePosition = pos;
            position = p.Position + pos;
            width = w;
            height = h;
            rect = new Rectangle((int)position.X, (int)position.Y, w, h);
            scrollDist = 0;
            yOff = 0;
            scrollClick = false;
        }
        #endregion

        #region Update, Handle Input, Draw
        public override void HandleInput(Input.Input input)
        {
            if (active)
            {
                InputState leftState = input.GetMouseState(MouseButtons.Left);
                if (scrollRect.Contains(input.MouseX(), input.MouseY()))
                {
                    if (!scrollClick && leftState == InputState.Pressed)
                    {
                        scrollClick = true;
                        yOff = input.MouseY() - scrollRect.Y;
                        xOff = input.MouseX() - scrollRect.X;
                    }
                }
                if (scrollClick && (leftState == InputState.Held || leftState == InputState.Pressed))
                {
                    scrollRect.Y = input.MouseY() - yOff;
                    if (scrollRect.Y < position.Y)
                    {
                        scrollRect.Y = (int)position.Y;
                    }
                    else if (scrollRect.Y > position.Y + height - scrollRect.Height)
                    {
                        scrollRect.Y = (int)(position.Y + height - scrollRect.Height);
                    }
                    scrollDist = (int)((position.Y - scrollRect.Y) / scale);
                    foreach (UIElement e in children)
                    {
                        e.UpdatePosition(new Vector2(0, scrollDist));
                    }
                }
                if (rect.Contains(input.MouseX(), input.MouseY()))
                {
                    if (!(scrollClick && (leftState == InputState.Held || leftState == InputState.Pressed)))
                    {
                        scrollClick = false;
                        int scrollNum = input.ScrollVal();
                        if (scrollNum != 0)
                        {
                            if (scrollNum > 0)
                            {
                                scrollDist -= 8;
                                if (scrollDist < -extraHeight)
                                {
                                    scrollDist = -extraHeight;
                                }
                            }
                            if (scrollNum < 0)
                            {
                                scrollDist += 8;
                                if (scrollDist > 0)
                                {
                                    scrollDist = 0;
                                }
                            }
                            scrollRect.Y = (int)(position.Y - (scrollDist * scale));
                            UpdateChildPosition(new Vector2(0, scrollDist));
                        }
                    }
                    if (!scrollClick)
                    {
                        HandleInputChildren(input);
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
                sBatch.Draw(tabImage, scrollRect, tint * alpha);

                sBatch.GraphicsDevice.ScissorRectangle = rect;
                DrawChildren(gameTime, sBatch);
                sBatch.GraphicsDevice.ScissorRectangle = oldRect;
            }

        }
        #endregion

        #region Public Functions
        public void CalculateMaxHeight()
        {
            int tempMax = 0;
            foreach (UIElement e in children)
            {
                if ((e.Rect.Height + e.RelativePosition.Y) > tempMax)
                {
                    tempMax = (int)(e.Rect.Height + e.RelativePosition.Y);
                }
            }
            extraHeight = tempMax-height;
            if (extraHeight < 0)
            {
                extraHeight = 0;
            }
            scale = (float)height / (float)(height + extraHeight);
            scrollRect = new Rectangle((int)position.X + width - 10, (int)position.Y, 10, (int)(height*scale));
        }
        #endregion
    }
}

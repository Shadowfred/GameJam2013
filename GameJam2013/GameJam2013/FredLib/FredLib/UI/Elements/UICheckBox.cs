using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FredLib.Input;

namespace FredLib.UI.Elements
{
    public class UICheckBox : UIElement
    {
        #region Attributes
        Texture2D image;
        /// <summary>
        /// The image to be drawn if specified.
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        bool selected = false;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        #endregion

        #region Initialisation
        /// <summary>
        /// Creates a new check box.
        /// </summary>
        /// <param name="i">The image used to draw the check box.</param>
        /// <param name="pos">The relative position of the check box.</param>
        public UICheckBox(Texture2D i, Vector2 pos)
            : base()
        {
            image = i;
            relativePosition = pos;
            position = pos;
            width = image.Width / 2;
            height = image.Height / 2;
            rect = new Rectangle((int)(position.X), (int)(position.Y),width,height);
        }

        /// <summary>
        /// Creates a new check box.
        /// </summary>
        /// <param name="p">The parent of the check box.</param>
        /// <param name="i">The image used to draw the check box.</param>
        /// <param name="pos">The relative position of the check box.</param>
        public UICheckBox(UIElement p, Texture2D i, Vector2 pos)
            : base(p)
        {
            image = i;
            relativePosition = pos;
            position = pos+parent.Position;
            width = image.Width / 2;
            height = image.Height / 2;
            rect = new Rectangle((int)(position.X), (int)(position.Y), width, height);
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
                if (state != UIState.Disabled)
                {
                    InputState mouseState = input.GetMouseState(MouseButtons.Left);
                    if (rect.Contains(input.MouseX(), input.MouseY()))
                    {
                        if (mouseState == InputState.Pressed || mouseState == InputState.Held)
                        {
                            if (state != UIState.Clicked && state != UIState.Held)
                            {
                                state = UIState.Clicked;
                                OnClick();
                            }
                            else
                            {
                                state = UIState.Held;
                            }
                        }
                        else
                        {
                            state = UIState.MouseOver;
                            if (mouseState == InputState.Released)
                            {
                                selected = !selected;
                                OnRelease();
                            }
                        }

                        HandleInputChildren(input);
                    }
                    else
                    {
                        state = UIState.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun.</param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (active)
            {
                int xPos = 0, yPos = 0;

                if (selected)
                {
                    yPos = height;
                }
                if (state == UIState.Clicked || state == UIState.Held)
                {
                    xPos = width;
                }
                Rectangle drawRect = new Rectangle(xPos, yPos, width, height);
                sBatch.Draw(image, rect, drawRect, tint * alpha);

                DrawChildren(gameTime, sBatch);
            }
        }
        #endregion
    }
}

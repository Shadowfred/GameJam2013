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
    /// A button to be clicked.
    /// </summary>
    public class UIButton : UIElement
    {
        #region Attributes
        /// <summary>
        /// The default image to be used when drawing, unless told otherwise.
        /// </summary>
        public static Texture2D defaultImage;
        /// <summary>
        /// The font to use when writing to the button.
        /// </summary>
        public static SpriteFont font;

        Texture2D image;
        /// <summary>
        /// The image to be drawn if specified.
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        string text;
        /// <summary>
        /// Can be used to write to the button.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// The position on the button, to draw the text.
        /// </summary>
        Vector2 textPos;

        bool useDefault = true;
        /// <summary>
        /// A bool to use the default button image or not.
        /// </summary>
        public bool UseDefault
        {
            get { return useDefault; }
            set { useDefault = value; }
        }

        bool useFont = true;
        /// <summary>
        /// A bool to use a string for the text or not.
        /// </summary>
        public bool UseFont
        {
            get { return useFont; }
            set { useFont = value; }
        }

        protected Color textTint = Color.Black;
        public Color TextTint
        {
            get { return textTint; }
            set { textTint = value; }
        }
        #endregion

        #region Initialisation
        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="i">The image used to draw the button.</param>
        /// <param name="pos">The relative position of the button.</param>
        public UIButton(Texture2D i, Vector2 pos)
            : base()
        {
            image = i;
            relativePosition = pos;
            position = pos;
            width = image.Width;
            height = image.Height / 5;
            useFont = false;
            useDefault = false;
            rect = new Rectangle((int)(position.X), (int)(position.Y),width,height);
        }

        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="p">The parent of the button.</param>
        /// <param name="i">The image used to draw the button.</param>
        /// <param name="pos">The relative position of the button.</param>
        public UIButton(UIElement p, Texture2D i, Vector2 pos)
            : base(p)
        {
            image = i;
            relativePosition = pos;
            position = pos+parent.Position;
            width = image.Width;
            height = image.Height / 5;
            useFont = false;
            useDefault = false;
            rect = new Rectangle((int)(position.X), (int)(position.Y), width, height);
        }

        /// <summary>
        /// Creates a new button using the default texture and font.
        /// </summary>
        /// <param name="p">The parent of the button.</param>
        /// <param name="pos">The relative position of the button.</param>
        /// <param name="t">The text to use on the button.</param>
        public UIButton(UIElement p, Vector2 pos, string t)
            : base(p)
        {
            text = t;
            relativePosition = pos;
            position = pos + parent.Position;
            width = defaultImage.Width;
            height = defaultImage.Height / 5;
            rect = new Rectangle((int)(position.X), (int)(position.Y), width, height);
            CalcTextPos();
        }

        /// <summary>
        /// Creates a new button using the default font.
        /// </summary>
        /// <param name="p">The parent of the button.</param>
        /// <param name="i">The image used to draw the button.</param>
        /// <param name="pos">The relative position of the button.</param>
        /// <param name="t">The text to use on the button.</param>
        public UIButton(UIElement p, Texture2D i, Vector2 pos, string t)
            : base(p)
        {
            text = t;
            image = i;
            relativePosition = pos;
            position = pos + parent.Position;
            width = image.Width;
            height = image.Height / 5;
            useDefault = false;
            rect = new Rectangle((int)(position.X), (int)(position.Y), width, height);
            CalcTextPos();
        }

        /// <summary>
        /// Creates a new button using the default texture with no text.
        /// </summary>
        /// <param name="p">The parent of the button.</param>
        /// <param name="pos">The relative position of the button.</param>
        public UIButton(UIElement p, Vector2 pos)
            : base(p)
        {
            relativePosition = pos;
            position = pos + parent.Position;
            width = defaultImage.Width;
            height = defaultImage.Height / 5;
            useFont = false;
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
                Texture2D drawImage;
                if (useDefault)
                {
                    drawImage = defaultImage;
                }
                else
                {
                    drawImage = image;
                }
                switch (state)
                {
                    case UIState.Default:
                        //First image
                        sBatch.Draw(drawImage, rect, new Rectangle(0, drawImage.Height * 0 / 5, drawImage.Width, drawImage.Height / 5), tint * alpha);
                        break;
                    case UIState.MouseOver:
                        //Second image
                        sBatch.Draw(drawImage, rect, new Rectangle(0, drawImage.Height * 1 / 5, drawImage.Width, drawImage.Height / 5), tint * alpha);
                        break;
                    case UIState.Clicked:
                        //Third Image
                        sBatch.Draw(drawImage, rect, new Rectangle(0, drawImage.Height * 2 / 5, drawImage.Width, drawImage.Height / 5), tint * alpha);
                        break;
                    case UIState.Held:
                        //Fourth Image
                        sBatch.Draw(drawImage, rect, new Rectangle(0, drawImage.Height * 3 / 5, drawImage.Width, drawImage.Height / 5), tint * alpha);
                        break;
                    default:
                        //Fifth Image
                        sBatch.Draw(drawImage, rect, new Rectangle(0, drawImage.Height * 4 / 5, drawImage.Width, drawImage.Height / 5), tint * alpha);
                        break;
                }
                if (useFont)
                {
                    sBatch.DrawString(font, text, position + textPos, textTint * alpha);
                }

                DrawChildren(gameTime, sBatch);
            }
        }

        /// <summary>
        /// Calculates the position to draw the text.
        /// </summary>
        public void CalcTextPos()
        {
            Vector2 dimensions = font.MeasureString(text);

            textPos = new Vector2((int)((width-dimensions.X)/2),(int)((height-dimensions.Y)/2));
        }
        #endregion
    }
}

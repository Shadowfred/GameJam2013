using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FredLib.Input;
using FredLib.Utilities;

namespace FredLib.UI.Elements
{
    /// <summary>
    /// A text box to enter text and display text.
    /// </summary>
    public class UITextBox : UIElement
    {
        #region Attributes

        protected bool editable;
        /// <summary>
        /// Can the textbox be edited by a user.
        /// </summary>
        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }

        protected bool selected;
        /// <summary>
        /// Is the textbox the currently selected textbox.
        /// </summary>
        public bool Selected
        {
            get {return selected;}
            set {selected = value;}
        }

        protected Texture2D image;
        /// <summary>
        /// The background of the textbox.
        /// </summary>
        public Texture2D Image
        {
            get {return image;}
            set {image = value;}
        }
        
        protected SpriteFont font;
        /// <summary>
        /// The font used to write the text.
        /// </summary>
        public SpriteFont Font
        {
            get {return font;}
            set {font = value;}
        }

        /// <summary>
        /// The text of the textbox.
        /// </summary>
        public string text;

        int textPos;

        /// <summary>
        /// The width of the textbox.
        /// </summary>
        public int Width
        {
            get {return width;}
            set {width = value;}
        }

        /// <summary>
        /// The height of the textbox.
        /// </summary>
        public int Height
        {
            get {return height;}
            set {height = value;}
        }

        protected Color textTint = Color.DarkBlue;
        public Color TextTint
        {
            get { return textTint; }
            set { textTint = value; }
        }

        protected bool textOnly = false;
        public bool TextOnly
        {
            get { return textOnly; }
            set { textOnly = value; }
        }
        #endregion

        #region Initialisation
        /// <summary>
        /// Creates a new tex box.
        /// </summary>
        /// <param name="i">The background of the text box.</param>
        /// <param name="f">The font to be used in the text box.</param>
        /// <param name="pos">The position of the text box.</param>
        /// <param name="w">The width of the text box.</param>
        /// <param name="h">The height of the text box.</param>
        public UITextBox(Texture2D i, SpriteFont f, Vector2 pos, int w, int h)
            : base()
        {
            image = i;
            relativePosition = pos;
            position = pos;
            width = w;
            height = h;
            font = f;
            text = "";
            rect = new Rectangle((int)pos.X, (int)pos.Y, w, h);
            selected = false;
            editable = false;
            textPos = 0;
        }

        /// <summary>
        /// Creates a new tex box.
        /// </summary>
        /// <param name="p">The parent of the text box.</param>
        /// <param name="i">The background of the text box.</param>
        /// <param name="f">The font to be used in the text box.</param>
        /// <param name="pos">The position of the text box.</param>
        /// <param name="w">The width of the text box.</param>
        /// <param name="h">The height of the text box.</param>
        public UITextBox(UIElement p, Texture2D i, SpriteFont f, Vector2 pos, int w, int h)
            : base(p)
        {
            image = i;
            relativePosition = pos;
            position = parent.Position + pos;
            width = w;
            height = h;
            font = f;
            text = "";
            rect = new Rectangle((int)(position.X), (int)(position.Y), w, h);
            selected = false;
            editable = false;
            textPos = 0;
        }

        #endregion

        #region Events
        public event EventHandler Enter; 
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
                HandleKeyInput(input);
                if (rect.Contains(input.MouseX(), input.MouseY()))
                {
                    InputState mouseState = input.GetMouseState(MouseButtons.Left);
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

        public void HandleKeyInput(Input.Input input)
        {
            Keys[] pressed = input.GetPressedKeys();

            if (selected && editable)
            {
                if (pressed.Length > 0) // dont do anything if keys have not been pressed
                {
                    for (int i = 0; i < pressed.Length; i++)
                    {
                        if (input.GetKeyState(pressed[i]) == InputState.Pressed)
                        {
                            if (KeyConverter.IsTypeableKey(pressed[i])) // no unicode characters
                            {
                                bool shift = input.IsKeyDown(Keys.RightShift) || input.IsKeyDown(Keys.LeftShift);
                                int asciiKey = KeyConverter.ConvertToAscii(pressed[i],shift);

                                if (!shift && asciiKey >= 65 && asciiKey <= 90)
                                {
                                    text = StringManip.AddCharAtPos(text, (char)(asciiKey + 32), textPos); // lower case
                                }
                                else
                                {
                                    text = StringManip.AddCharAtPos(text, (char)asciiKey, textPos);
                                }
                                textPos++;
                            }
                            else if (pressed[i] == Keys.Back) // explicitly handle backspace
                            {
                                if (text.Length > 0)
                                {
                                    text = StringManip.RemoveCharAtPos(text, textPos);
                                    textPos--;
                                    if (textPos < 0)
                                    {
                                        textPos = 0;
                                    }
                                }
                            }
                            else if (pressed[i] == Keys.Delete)
                            {
                                if (text.Length > 0 && textPos != text.Length)
                                {
                                    text = StringManip.RemoveCharAtPos(text, textPos + 1);
                                    if (textPos > text.Length)
                                    {
                                        textPos = text.Length;
                                    }
                                }
                            }
                            else if (pressed[i] == Keys.Enter) // explicitly handle enter
                            {
                                if (Enter != null)
                                {
                                    Enter(null, new EventArgs());
                                }
                                else
                                {
                                    text += '\n';
                                }
                            }
                            else if (pressed[i] == Keys.Left)
                            {
                                textPos--;
                                if (textPos < 0)
                                {
                                    textPos = 0;
                                }
                            }
                            else if (pressed[i] == Keys.Right)
                            {
                                textPos++;
                                if (textPos > text.Length)
                                {
                                    textPos = text.Length;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws the text box.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun. Requires ScissorTestEnable to be true in the RasterizerState.</param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (active)
            {
                if (rect.Top >= 0 && rect.Bottom <= sBatch.GraphicsDevice.Viewport.Height)
                {
                    Rectangle currentRect = sBatch.GraphicsDevice.ScissorRectangle;
                    if (!textOnly)
                    {
                        sBatch.Draw(image, position, new Rectangle(0, 0, width, height), tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                        if (selected)
                        {
                            if (editable && gameTime.TotalGameTime.Milliseconds >= 500)
                            {
                                string underline = "";
                                for (int i = 0; i < textPos; i++)
                                {
                                    underline += ' ';
                                }
                                underline += '_';
                                sBatch.GraphicsDevice.ScissorRectangle = rect;
                                sBatch.DrawString(font, underline, position + new Vector2(3, 3), textTint * alpha);
                                sBatch.GraphicsDevice.ScissorRectangle = currentRect;
                            }
                            //sBatch.DrawString(font, "ME", position + new Vector2(3, 3) + new Vector2(rect.Width, 0), Color.Yellow * alpha);
                        }
                    }

                    sBatch.GraphicsDevice.ScissorRectangle = rect;
                    sBatch.DrawString(font, text, position + new Vector2(3, 3), textTint * alpha);
                    sBatch.GraphicsDevice.ScissorRectangle = currentRect;
                    DrawChildren(gameTime, sBatch);
                }
            }
        }

        public void ClearText()
        {
            text = "";
            textPos = 0;
        }

        #endregion
    }
}

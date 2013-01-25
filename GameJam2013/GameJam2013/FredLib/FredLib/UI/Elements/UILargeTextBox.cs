using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FredLib.UI.Elements
{
    /// <summary>
    /// A large text box that handles multiple lines.
    /// </summary>
    public class UILargeTextBox : UITextBox
    {
        #region Attributes
        public List<string> textLines;

        public bool drawBack = true;
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
        public UILargeTextBox(Texture2D i, SpriteFont f, Vector2 pos, int w, int h)
            : base(i, f, pos, w, h)
        {
            textLines = new List<string>();
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
        public UILargeTextBox(UIElement p, Texture2D i, SpriteFont f, Vector2 pos, int w, int h)
            : base(p, i, f, pos, w, h)
        {
            font.LineSpacing = (int)font.MeasureString("l").Y;
            textLines = new List<string>();
        }
        #endregion

        #region Update, Handle Input, Draw
        /// <summary>
        /// Draws the text box.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun. Requires ScissorRectangle to be enabled in the RasterizerState.</param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (active)
            {
                Rectangle currentRect = sBatch.GraphicsDevice.ScissorRectangle;
                if (drawBack)
                {
                    sBatch.Draw(image, position, new Rectangle(0, 0, width, height), tint * alpha, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }

                sBatch.GraphicsDevice.ScissorRectangle = rect;
                int count = 0;
                int yval = 0;
                for (int i = textLines.Count - 1; i >= 0; i--)
                {
                    // take nulls and things off
                    string toDraw = textLines.ElementAt(i).Trim('\0');
                    yval += (int)font.MeasureString(toDraw).Y;
                    sBatch.DrawString(font, toDraw, position + new Vector2(3, height - yval), textTint * alpha);
                    //sBatch.DrawString(font, textLines.ElementAt(i), position + new Vector2(3, height - font.MeasureString(textLines.ElementAt(i)).Y - count * textSize), Color.DarkBlue);
                    if (font.MeasureString(toDraw).Y > 20)
                    {
                        count++;
                    }
                    count++;
                }
                sBatch.GraphicsDevice.ScissorRectangle = currentRect;
            }
        }
        #endregion
    }
}
